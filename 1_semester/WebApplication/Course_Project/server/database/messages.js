const {getDb} = require("./db");

const TABLE_NAME = "Messages";

module.exports = {
    TABLE_NAME,
    createMessage: async (text, userId, chatId) => {
        const result = await getDb().run(
            `INSERT INTO ${TABLE_NAME} (Text, FK_User, FK_Chat) VALUES (?, ?, ?)`,
            text, userId, chatId
        );
        
        // Получаем созданное сообщение с информацией о пользователе
        const message = await getDb().get(`
            SELECT m.*, u.Username, u.Nickname 
            FROM ${TABLE_NAME} m 
            JOIN Users u ON m.FK_User = u.ID 
            WHERE m.ID = ?
        `, result.lastID);
        
        return message;
    },
    
    getChatMessages: async (chatId) => {
        return await getDb().all(`
            SELECT m.*, u.Username, u.Nickname 
            FROM ${TABLE_NAME} m 
            JOIN Users u ON m.FK_User = u.ID 
            WHERE m.FK_Chat = ? 
            ORDER BY m.Created_at ASC
        `, chatId);
    },
    
    getMessageById: async (id) => {
        return await getDb().get(`
            SELECT m.*, u.Username, u.Nickname 
            FROM ${TABLE_NAME} m 
            JOIN Users u ON m.FK_User = u.ID 
            WHERE m.ID = ?
        `, id);
    }
}