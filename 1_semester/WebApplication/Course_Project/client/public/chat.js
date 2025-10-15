let ws = null;
let currentUser = null;
let currentChatId = null;

async function login() {
    const login = document.getElementById('loginInput').value;
    const password = document.getElementById('passwordInput').value;
    
    try {
        const response = await fetch('/api/login', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ login, password })
        });
        
        const data = await response.json();
        
        if (response.ok) {
            currentUser = data.user;
            connectWebSocket(data.token);
            loadChats();
            document.getElementById('authSection').style.display = 'none';
            document.getElementById('chatSection').style.display = 'block';
        } else {
            alert(data.error);
        }
    } catch (error) {
        alert('Ошибка соединения');
    }
}

async function register() {
    const login = document.getElementById('loginInput').value;
    const password = document.getElementById('passwordInput').value;
    
    try {
        const response = await fetch('/api/register', {
            method: 'POST',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify({ login, password })
        });
        
        const data = await response.json();
        
        if (response.ok) {
            alert('Регистрация успешна! Теперь войдите.');
        } else {
            alert(data.error);
        }
    } catch (error) {
        alert('Ошибка соединения');
    }
}

function connectWebSocket(token) {
    ws = new WebSocket(`ws://localhost:3000`);
    
    ws.onopen = () => {
        console.log('WebSocket connected');
        // Аутентифицируемся
        ws.send(JSON.stringify({
            type: 'auth',
            token: token
        }));
    };
    
    ws.onmessage = (event) => {
        const message = JSON.parse(event.data);
        handleWebSocketMessage(message);
    };
    
    ws.onclose = () => {
        console.log('WebSocket disconnected');
    };
}

function handleWebSocketMessage(message) {
    switch (message.type) {
        case 'new_message':
            if (message.message.FK_Chat === currentChatId) {
                displayMessage(message.message);
            }
            break;
        case 'user_status_changed':
            console.log(`User ${message.userId} is now ${message.status}`);
            break;
        case 'auth_success':
            console.log('WebSocket auth success');
            break;
        case 'error':
            alert(message.error);
            break;
    }
}

async function loadChats() {
    try {
        const token = localStorage.getItem('token');
        const response = await fetch('/api/chats', {
            headers: { 'Authorization': token }
        });
        
        const data = await response.json();
        
        if (response.ok) {
            displayChats(data.chats);
        }
    } catch (error) {
        console.error('Error loading chats:', error);
    }
}

function displayChats(chats) {
    const container = document.getElementById('chatsContainer');
    container.innerHTML = '';
    
    chats.forEach(chat => {
        const chatElement = document.createElement('div');
        chatElement.innerHTML = `
            <div style="padding: 10px; border-bottom: 1px solid #eee; cursor: pointer;" 
                 onclick="selectChat(${chat.ID}, '${chat.Name}')">
                <strong>${chat.Name}</strong>
                <br>
                <small>Автор: ${chat.author_name}</small>
            </div>
        `;
        container.appendChild(chatElement);
    });
}

async function selectChat(chatId, chatName) {
    currentChatId = chatId;
    document.getElementById('messagesContainer').innerHTML = `<h3>${chatName}</h3>`;
    
    try {
        const token = localStorage.getItem('token');
        const response = await fetch(`/api/chats/${chatId}/messages`, {
            headers: { 'Authorization': token }
        });
        
        const data = await response.json();
        
        if (response.ok) {
            data.messages.forEach(message => {
                displayMessage(message);
            });
        }
    } catch (error) {
        console.error('Error loading messages:', error);
    }
}

function displayMessage(message) {
    const container = document.getElementById('messagesContainer');
    const messageElement = document.createElement('div');
    messageElement.className = 'message';
    messageElement.innerHTML = `
        <div class="author">${message.Username || message.Nickname}:</div>
        <div>${message.Text}</div>
        <div class="time">${new Date(message.Created_at).toLocaleTimeString()}</div>
    `;
    container.appendChild(messageElement);
    container.scrollTop = container.scrollHeight;
}

function sendMessage() {
    if (!ws || !currentChatId) {
        alert('Выберите чат');
        return;
    }
    
    const input = document.getElementById('messageInput');
    const text = input.value.trim();
    
    if (text) {
        ws.send(JSON.stringify({
            type: 'send_message',
            chatId: currentChatId,
            text: text
        }));
        input.value = '';
    }
}

async function createChat() {
    const name = prompt('Введите название чата:');
    if (name) {
        try {
            const token = localStorage.getItem('token');
            const response = await fetch('/api/chats', {
                method: 'POST',
                headers: { 
                    'Content-Type': 'application/json',
                    'Authorization': token 
                },
                body: JSON.stringify({ name })
            });
            
            const data = await response.json();
            
            if (response.ok) {
                loadChats();
                selectChat(data.chat.id, data.chat.name);
            } else {
                alert(data.error);
            }
        } catch (error) {
            alert('Ошибка создания чата');
        }
    }
}

// Enter для отправки сообщения
document.getElementById('messageInput').addEventListener('keypress', function(e) {
    if (e.key === 'Enter') {
        sendMessage();
    }
});