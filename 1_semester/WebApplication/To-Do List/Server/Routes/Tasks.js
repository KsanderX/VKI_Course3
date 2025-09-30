const express = require('express');
const Task = require('../Models/Task');
const router = express.Router();

// Получить все задачи
router.get('/', async (req, res) => {
    try {
        const { filter = 'all', sort = 'default' } = req.query;
        const tasks = await Task.getAll(filter, sort);
        res.json(tasks);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Добавить новую задачу
router.post('/', async (req, res) => {
    try {
        const { text } = req.body;
        
        if (!text || text.trim() === '') {
            return res.status(400).json({ error: 'Текст задачи обязателен' });
        }
        
        const task = await Task.create({ text });
        res.status(201).json(task);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Обновить задачу
router.put('/:id', async (req, res) => {
    try {
        const { id } = req.params;
        const { text, done } = req.body;
        
        const updates = {};
        if (text !== undefined) updates.text = text;
        if (done !== undefined) updates.done = done;
        
        const task = await Task.update(id, updates);
        res.json(task);
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Удалить задачу
router.delete('/:id', async (req, res) => {
    try {
        const { id } = req.params;
        await Task.delete(id);
        res.json({ message: 'Задача удалена' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

// Удалить все задачи
router.delete('/', async (req, res) => {
    try {
        await Task.deleteAll();
        res.json({ message: 'Все задачи удалены' });
    } catch (error) {
        res.status(500).json({ error: error.message });
    }
});

module.exports = router;