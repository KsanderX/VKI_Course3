const db = require('../DB/db');

class Task {
    static async getAll(filter = 'all', sort = 'default') {
        let query = 'SELECT * FROM tasks';
        let params = [];
        
        // Фильтрация
        if (filter === 'done') {
            query += ' WHERE done = 1';
        } else if (filter === 'not-done') {
            query += ' WHERE done = 0';
        }
        
        // Сортировка
        if (sort === 'name') {
            query += ' ORDER BY text ASC';
        } else if (sort === 'date') {
            query += ' ORDER BY date DESC';
        } else {
            query += ' ORDER BY created_at DESC';
        }
        
        return await db.all(query, params);
    }

    static async getById(id) {
        const tasks = await db.all('SELECT * FROM tasks WHERE id = ?', [id]);
        return tasks[0] || null;
    }

    static async create(taskData) {
        const { text } = taskData;
        const task = {
            text: text.trim(),
            done: false,
            date: new Date().toLocaleDateString()
        };
        
        const result = await db.run(
            'INSERT INTO tasks (text, done, date) VALUES (?, ?, ?)',
            [task.text, task.done ? 1 : 0, task.date]
        );
        
        return {
            id: result.id,
            ...task
        };
    }

    static async update(id, updates) {
        if (updates.text !== undefined) {
            await db.run(
                'UPDATE tasks SET text = ? WHERE id = ?',
                [updates.text, id]
            );
        }
        
        if (updates.done !== undefined) {
            await db.run(
                'UPDATE tasks SET done = ? WHERE id = ?',
                [updates.done ? 1 : 0, id]
            );
        }
        
        return await this.getById(id);
    }

    static async delete(id) {
        await db.run('DELETE FROM tasks WHERE id = ?', [id]);
        return true;
    }

    static async deleteAll() {
        await db.run('DELETE FROM tasks');
        return true;
    }
}

module.exports = Task;