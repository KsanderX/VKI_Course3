const {getDb} = require("./db");

const TABLE_NAME = "Chats";

module.exports = {
    TABLE_NAME,
    createChat: async (name, authorId) => {
        const result = await getDb().run(
            `INSERT INTO ${TABLE_NAME} (Name, Author_ID) VALUES (?, ?)`,
            name, authorId
        );
        
        // Добавляем автора в участники
        await getDb().run(
            `INSERT INTO Chat_Participants (FK_User, FK_Chat) VALUES (?, ?)`,
            authorId, result.lastID
        );
        
        return { id: result.lastID, name, author_id: authorId };
    },
    
    getChatById: async (id) => {
        return await getDb().get(`
            SELECT c.*, u.Username as author_name 
            FROM ${TABLE_NAME} c 
            JOIN Users u ON c.Author_ID = u.ID 
            WHERE c.ID = ?
        `, id);
    },
    
    getAllChats: async () => {
        return await getDb().all(`
            SELECT c.*, u.Username as author_name 
            FROM ${TABLE_NAME} c 
            JOIN Users u ON c.Author_ID = u.ID
        `);
    },
    
    getUserChats: async (userId) => {
        return await getDb().all(`
            SELECT c.*, u.Username as author_name 
            FROM ${TABLE_NAME} c 
            JOIN Chat_Participants cp ON c.ID = cp.FK_Chat 
            JOIN Users u ON c.Author_ID = u.ID
            WHERE cp.FK_User = ?
        `, userId);
    }
}