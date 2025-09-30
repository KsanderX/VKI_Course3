const sqlite3 = require('sqlite3');
const {open} = require('sqlite');

let db;

const initDb = async () => {
    if (!db) {
        db = await open({
            filename: 'database.db',
            drivar: sqlite3.Database
        })
    }

    await db.exec(`
        CREATE TABLE IF NOT EXISTS Users (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT,
            Nickname TEXT unique,
            Login TEXT NOT NULL,
            Password TEXT NOT NULL,
            FK_Status foreign key references User_Status(ID)
        )`);

    await db.exec(` 
        CREATE TABLE IF NOT EXISTS Chats (
            ID integer primary key autoincrement,
            Name  text not null,
            Created_at datetime defauld current_timestamp,
        )`)

    await db.exec(`
        Create table if not exists Participants (
            ID integer primary key autoincrement,
            FK_User foreign key references Users(ID),
            FK_Chat foreign key references Chats(ID),
            UNIQUE(FK_User, FK_Chat)    
        )`)

    await db.exec(`
        Create table if not exists Messages (
            ID integer primary key autoincrement,
            Text text not null,
            Created_at datetime defauld current_timestamp,
            FK_User foreign key references Users(ID),
            FK_Chat foreign key references Chats(ID),
            UNIQUE(FK_User, FK_Chat)    
        )`)
    
    await db.exec(`
        Create table if not exists User_Satus (
            ID integer primary key autoincrement,
            Name_status text not null
        )`)
};

const getDb = () => db;

module.export = {
    initDb,
    getDb
}