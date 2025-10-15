const {getDb} = require("./db");
const md5 = require('md5');

const TABLE_NAME = "Users";

module.exports = {
    TABLE_NAME,  
    addUser: async (login, password, username, nickname) => {
        const result = await getDb().run(
            `INSERT INTO ${TABLE_NAME} (Login, Password, Username, Nickname) VALUES (?, ?, ?, ?)`,
            login, md5(password), username, nickname
        );
        return { id: result.lastID, login, username, nickname };
    },
    
    getUserByLogin: async (login) => {
        return await getDb().get(
            `SELECT * FROM ${TABLE_NAME} WHERE Login = ?`,
            login
        );
    },
    
    getUserById: async (id) => {
        return await getDb().get(
            `SELECT * FROM ${TABLE_NAME} WHERE ID = ?`,
            id
        );
    },
    
    getAllUsers: async () => {
        return await getDb().all(`SELECT * FROM ${TABLE_NAME}`);
    },
    
    updateUserStatus: async (userId, statusId) => {
        await getDb().run(
            `UPDATE ${TABLE_NAME} SET FK_Status = ? WHERE ID = ?`,
            statusId, userId
        );
    }
}