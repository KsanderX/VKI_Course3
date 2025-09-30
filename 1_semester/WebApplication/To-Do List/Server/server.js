const express = require('express');
const cors = require('cors');
const path = require('path');
const taskRoutes = require('./Routes/Tasks');

const app = express();
const PORT = process.env.PORT || 3000;

// Middleware
app.use(cors());
app.use(express.json());

// Serve static files from client directory
app.use(express.static(path.join(__dirname, '..', 'client')));

// API routes
app.use('/api/tasks', taskRoutes);

// Serve the main page
app.get('/', (req, res) => {
    res.sendFile(path.join(__dirname, '..', 'client', 'To-doList.html'));
});

// Handle 404
app.use('*', (req, res) => {
    res.status(404).json({ error: 'Маршрут не найден' });
});

// Error handling middleware
app.use((err, req, res, next) => {
    console.error('Ошибка сервера:', err);
    res.status(500).json({ error: 'Внутренняя ошибка сервера' });
});

app.listen(PORT, () => {
    console.log(`Сервер запущен на порту ${PORT}`);
    console.log(`Откройте http://localhost:${PORT} в браузере`);
});