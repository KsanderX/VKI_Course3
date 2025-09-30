const sqlite3 = require('sqlite3').verbose();
const path = require('path');

const dbPath = path.join(__dirname, '..', '..', 'todos.db');

class Database {
    constructor() {
        this.db = new sqlite3.Database(dbPath, (err) => {
            if (err) {
                console.error('Ошибка подключения к БД:', err.message);
            } else {
                console.log('Подключено к SQLite базе данных');
                this.initDatabase();
            }
        });
    }

    initDatabase() {
        this.db.run(`CREATE TABLE IF NOT EXISTS tasks (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            text TEXT NOT NULL,
            done BOOLEAN DEFAULT 0,
            date TEXT NOT NULL,
            created_at DATETIME DEFAULT CURRENT_TIMESTAMP
        )`);
    }

    all(sql, params = []) {
        return new Promise((resolve, reject) => {
            this.db.all(sql, params, (err, rows) => {
                if (err) {
                    reject(err);
                } else {
                    resolve(rows);
                }
            });
        });
    }

    run(sql, params = []) {
        return new Promise((resolve, reject) => {
            this.db.run(sql, params, function(err) {
                if (err) {
                    reject(err);
                } else {
                    resolve({ id: this.lastID, changes: this.changes });
                }
            });
        });
    }

    close() {
        return new Promise((resolve, reject) => {
            this.db.close((err) => {
                if (err) {
                    reject(err);
                } else {
                    resolve();
                }
            });
        });
    }
}

module.exports = new Database();