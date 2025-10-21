const sqlite3 = require('sqlite3');
const {open} = require('sqlite');

let db;

const initDb = async () => {
    if (!db) {
        db = await open({
            filename: '/database.db',
            driver: sqlite3.Database
        })
    }
   
    await db.exec(`
        Create table if not exists User_Status (
            ID integer primary key autoincrement,
            Status_name text not null
        )`)

    await db.run(`insert or ignore into User_Status (Status_name) values ('online')`);
    await db.run(`insert or ignore into User_Status (Status_name) values ('offline')`);

    await db.exec(`
        CREATE TABLE IF NOT EXISTS Users (
            ID INTEGER PRIMARY KEY AUTOINCREMENT,
            Username TEXT,
            Nickname TEXT unique,
            Login TEXT NOT NULL,
            Password TEXT NOT NULL,
            FK_Status integer default 2,
            Foreign key (FK_Status) references User_Status(ID)
        )`);

    await db.exec(` 
        CREATE TABLE IF NOT EXISTS Chats (
            ID integer primary key autoincrement,
            Name  text not null,
            Author_ID integer not null,
            Created_at datetime defauld current_timestamp,
            Foreign key (Author_ID) references Users(ID)
        )`)

    await db.exec(`
        Create table if not exists Chat_Participants (
            ID integer primary key autoincrement,
            Joined_at DATETIME DEFAULT CURRENT_TIMESTAMP,
            FK_User integer foreign key references Users(ID),
            FK_Chat integer foreign key references Chats(ID),
            UNIQUE(FK_User, FK_Chat)    
        )`)

    await db.exec(`
        Create table if not exists Messages (
            ID integer primary key autoincrement,
            Text text not null,
            Created_at datetime defauld current_timestamp,
            FK_User integer not null,
            Foreign key (FK_User) references Users(ID),
            FK_Chat integer not null,
            Foreign key (FK_Chat) references Chats(ID)
        )`)

    await db.exec(`
        CREATE TABLE IF NOT EXISTS tokens (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            userId INTEGER NOT NULL,
            token TEXT NOT NULL
        )`);
};

const getDb = () => db;

module.exports = {
    initDb,
    getDb
}