const express = require('express');
const http = require('http');
const WebSocket = require('ws');
const path = require('path');
const { initDb } = require('./db');
const usersDb = require('./db/users');
const tokensDb = require('./db/tokens');
const chatsDb = require('./db/chats');
const messagesDb = require('./db/messages');
const participantsDb = require('./db/chatParticipants');

const app = express();
const server = http.createServer(app);
const wss = new WebSocket.Server({ server });

app.use(express.json());
app.use(express.static(path.join(__dirname, 'public')));

// Хранилище активных соединений
const activeConnections = new Map(); // userId -> WebSocket

// Инициализация БД
initDb();

// REST API Routes

// Регистрация
app.post('/api/register', async (req, res) => {
    try {
        const { login, password, username, nickname } = req.body;
        
        if (!login || !password) {
            return res.status(400).json({ error: 'Login and password are required' });
        }
        
        const existingUser = await usersDb.getUserByLogin(login);
        if (existingUser) {
            return res.status(400).json({ error: 'User already exists' });
        }
        
        const user = await usersDb.addUser(login, password, username, nickname);
        const token = await tokensDb.addToken(user.id);
        
        res.json({ user: { id: user.id, login: user.login, username: user.username }, token });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Авторизация
app.post('/api/login', async (req, res) => {
    try {
        const { login, password } = req.body;
        const md5 = require('md5');
        
        const user = await usersDb.getUserByLogin(login);
        if (!user || user.Password !== md5(password)) {
            return res.status(401).json({ error: 'Invalid credentials' });
        }
        
        const token = await tokensDb.addToken(user.ID);
        
        res.json({ 
            user: { 
                id: user.ID, 
                login: user.Login, 
                username: user.Username,
                nickname: user.Nickname 
            }, 
            token 
        });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Получить чаты пользователя
app.get('/api/chats', async (req, res) => {
    try {
        const token = req.headers.authorization;
        if (!token) {
            return res.status(401).json({ error: 'Token required' });
        }
        
        const userId = await tokensDb.getUserIdByToken(token);
        if (!userId) {
            return res.status(401).json({ error: 'Invalid token' });
        }
        
        const chats = await chatsDb.getUserChats(userId);
        res.json({ chats });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Создать чат
app.post('/api/chats', async (req, res) => {
    try {
        const token = req.headers.authorization;
        const { name } = req.body;
        
        if (!token) {
            return res.status(401).json({ error: 'Token required' });
        }
        
        const userId = await tokensDb.getUserIdByToken(token);
        if (!userId) {
            return res.status(401).json({ error: 'Invalid token' });
        }
        
        const chat = await chatsDb.createChat(name, userId);
        res.json({ chat });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Получить сообщения чата
app.get('/api/chats/:chatId/messages', async (req, res) => {
    try {
        const token = req.headers.authorization;
        const { chatId } = req.params;
        
        if (!token) {
            return res.status(401).json({ error: 'Token required' });
        }
        
        const userId = await tokensDb.getUserIdByToken(token);
        if (!userId) {
            return res.status(401).json({ error: 'Invalid token' });
        }
        
        // Проверяем, что пользователь в чате
        const isInChat = await participantsDb.isUserInChat(userId, chatId);
        if (!isInChat) {
            return res.status(403).json({ error: 'Access denied' });
        }
        
        const messages = await messagesDb.getChatMessages(chatId);
        res.json({ messages });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// WebSocket обработка
wss.on('connection', (ws) => {
    console.log('New WebSocket connection');
    
    ws.on('message', async (data) => {
        try {
            const message = JSON.parse(data);
            
            switch (message.type) {
                case 'auth':
                    // Аутентификация по токену
                    const userId = await tokensDb.getUserIdByToken(message.token);
                    if (userId) {
                        activeConnections.set(userId, ws);
                        ws.userId = userId;
                        
                        // Обновляем статус на онлайн
                        await usersDb.updateUserStatus(userId, 1); // 1 = online
                        
                        // Уведомляем всех о новом онлайн пользователе
                        broadcastUserStatus(userId, 'online');
                        
                        ws.send(JSON.stringify({
                            type: 'auth_success',
                            user: { id: userId }
                        }));
                    } else {
                        ws.send(JSON.stringify({
                            type: 'auth_error',
                            error: 'Invalid token'
                        }));
                    }
                    break;
                    
                case 'send_message':
                    if (!ws.userId) {
                        ws.send(JSON.stringify({
                            type: 'error',
                            error: 'Not authenticated'
                        }));
                        return;
                    }
                    
                    const { chatId, text } = message;
                    
                    // Проверяем, что пользователь в чате
                    const isInChat = await participantsDb.isUserInChat(ws.userId, chatId);
                    if (!isInChat) {
                        ws.send(JSON.stringify({
                            type: 'error',
                            error: 'Not in chat'
                        }));
                        return;
                    }
                    
                    // Сохраняем сообщение в БД
                    const newMessage = await messagesDb.createMessage(text, ws.userId, chatId);
                    
                    // Рассылаем сообщение всем участникам чата
                    const participants = await participantsDb.getChatParticipants(chatId);
                    participants.forEach(participant => {
                        const connection = activeConnections.get(participant.ID);
                        if (connection) {
                            connection.send(JSON.stringify({
                                type: 'new_message',
                                message: newMessage
                            }));
                        }
                    });
                    break;
                    
                case 'join_chat':
                    if (!ws.userId) {
                        ws.send(JSON.stringify({
                            type: 'error',
                            error: 'Not authenticated'
                        }));
                        return;
                    }
                    
                    const { joinChatId } = message;
                    await participantsDb.addParticipant(ws.userId, joinChatId);
                    
                    ws.send(JSON.stringify({
                        type: 'chat_joined',
                        chatId: joinChatId
                    }));
                    break;
            }
        } catch (error) {
            console.error('WebSocket error:', error);
            ws.send(JSON.stringify({
                type: 'error',
                error: 'Internal server error'
            }));
        }
    });
    
    ws.on('close', async () => {
        if (ws.userId) {
            activeConnections.delete(ws.userId);
            
            // Обновляем статус на оффлайн
            await usersDb.updateUserStatus(ws.userId, 2); // 2 = offline
            
            // Уведомляем всех об оффлайн пользователе
            broadcastUserStatus(ws.userId, 'offline');
        }
    });
});

// Функция для рассылки статуса пользователя
function broadcastUserStatus(userId, status) {
    const statusMessage = JSON.stringify({
        type: 'user_status_changed',
        userId,
        status
    });
    
    activeConnections.forEach((connection, connectionUserId) => {
        if (connectionUserId !== userId) {
            connection.send(statusMessage);
        }
    });
}

// Статическая страница чата
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, 'public', 'index.html'));
});

const PORT = process.env.PORT || 3000;
server.listen(PORT, () => {
    console.log(`Server running on port ${PORT}`);
});