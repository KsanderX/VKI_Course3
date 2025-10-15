const {getDb} = require("./db");

const TABLE_NAME = "Chat_Participants";

module.exports = {
    TABLE_NAME,
    addParticipant: async (userId, chatId) => {
        const result = await getDb().run(
            `INSERT INTO ${TABLE_NAME} (FK_User, FK_Chat) VALUES (?, ?)`,
            userId, chatId
        );
        return { id: result.lastID, user_id: userId, chat_id: chatId };
    },
    
    getChatParticipants: async (chatId) => {
        return await getDb().all(`
            SELECT u.ID, u.Username, u.Nickname, us.Status_Name 
            FROM Users u 
            JOIN ${TABLE_NAME} cp ON u.ID = cp.FK_User 
            JOIN User_Status us ON u.FK_Status = us.ID
            WHERE cp.FK_Chat = ?
        `, chatId);
    },
    
    isUserInChat: async (userId, chatId) => {
        const result = await getDb().get(
            `SELECT ID FROM ${TABLE_NAME} WHERE FK_User = ? AND FK_Chat = ?`,
            userId, chatId
        );
        return !!result;
    },
    
    removeParticipant: async (userId, chatId) => {
        await getDb().run(
            `DELETE FROM ${TABLE_NAME} WHERE FK_User = ? AND FK_Chat = ?`,
            userId, chatId
        );
    }
}