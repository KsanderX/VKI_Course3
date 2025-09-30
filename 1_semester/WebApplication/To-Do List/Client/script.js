document.addEventListener('DOMContentLoaded', () => {
    const taskList = document.getElementById('task-list');
    const newTaskInput = document.getElementById('new-task');
    const addTaskButton = document.getElementById('add-task');
    const clearAllButton = document.getElementById('clear-all');
    const filterLinks = document.querySelectorAll('.filter-link');
    const sortSelect = document.getElementById('sort-tasks');

    let currentFilter = 'all';
    let currentSort = 'default';

    // API functions
    const API_BASE = '/api/tasks';

    async function fetchTasks() {
        const params = new URLSearchParams({
            filter: currentFilter,
            sort: currentSort
        });
        
        try {
            const response = await fetch(`${API_BASE}?${params}`);
            if (!response.ok) throw new Error('Ошибка загрузки задач');
            return await response.json();
        } catch (error) {
            console.error('Error:', error);
            alert('Ошибка загрузки задач');
            return [];
        }
    }

    async function addTask(text) {
        try {
            const response = await fetch(API_BASE, {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ text })
            });
            
            if (!response.ok) throw new Error('Ошибка добавления задачи');
            return await response.json();
        } catch (error) {
            console.error('Error:', error);
            alert('Ошибка добавления задачи');
        }
    }

    async function deleteTask(id) {
        try {
            const response = await fetch(`${API_BASE}/${id}`, {
                method: 'DELETE'
            });
            
            if (!response.ok) throw new Error('Ошибка удаления задачи');
        } catch (error) {
            console.error('Error:', error);
            alert('Ошибка удаления задачи');
        }
    }

    async function toggleTaskDone(id, done) {
        try {
            const response = await fetch(`${API_BASE}/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ done })
            });
            
            if (!response.ok) throw new Error('Ошибка обновления задачи');
        } catch (error) {
            console.error('Error:', error);
            alert('Ошибка обновления задачи');
        }
    }

    async function updateTaskText(id, text) {
        try {
            const response = await fetch(`${API_BASE}/${id}`, {
                method: 'PUT',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify({ text })
            });
            
            if (!response.ok) throw new Error('Ошибка обновления задачи');
        } catch (error) {
            console.error('Error:', error);
            alert('Ошибка обновления задачи');
        }
    }

    async function clearAllTasks() {
        try {
            const response = await fetch(API_BASE, {
                method: 'DELETE'
            });
            
            if (!response.ok) throw new Error('Ошибка очистки задач');
        } catch (error) {
            console.error('Error:', error);
            alert('Ошибка очистки задач');
        }
    }

    async function renderTasks() {
        const tasks = await fetchTasks();
        taskList.innerHTML = '';

        tasks.forEach(task => {
            const li = document.createElement('li');
            li.innerHTML = `
                <input type="checkbox" ${task.done ? 'checked' : ''} data-id="${task.id}">
                <span contenteditable="true" class="editable" data-id="${task.id}">${task.text}</span>
                <sub>от ${task.date}</sub>
                <button class="edit-btn" data-id="${task.id}">💾</button>
                <button data-id="${task.id}">❌</button>
            `;
            taskList.appendChild(li);
        });
    }

    // Event Listeners
    addTaskButton.addEventListener('click', async () => {
        const text = newTaskInput.value.trim();
        if (text) {
            await addTask(text);
            newTaskInput.value = '';
            await renderTasks();
        }
    });

    clearAllButton.addEventListener('click', async () => {
        if (confirm('Вы уверены, что хотите удалить все задачи?')) {
            await clearAllTasks();
            await renderTasks();
        }
    });

    taskList.addEventListener('click', async (e) => {
        const id = e.target.dataset.id;
        
        if (e.target.tagName === 'BUTTON' && e.target.textContent === '❌') {
            await deleteTask(id);
            await renderTasks();
        } else if (e.target.tagName === 'INPUT') {
            await toggleTaskDone(id, e.target.checked);
            await renderTasks();
        } else if (e.target.classList.contains('edit-btn')) {
            const span = document.querySelector(`span[data-id="${id}"]`);
            await updateTaskText(id, span.textContent.trim());
            await renderTasks();
        }
    });

    filterLinks.forEach(link => {
        link.addEventListener('click', async (e) => {
            e.preventDefault();
            currentFilter = e.target.dataset.filter;
            filterLinks.forEach(l => l.classList.remove('active'));
            e.target.classList.add('active');
            await renderTasks();
        });
    });

    sortSelect.addEventListener('change', async (e) => {
        currentSort = e.target.value;
        await renderTasks();
    });

    // Enter key for adding tasks
    newTaskInput.addEventListener('keypress', async (e) => {
        if (e.key === 'Enter') {
            const text = newTaskInput.value.trim();
            if (text) {
                await addTask(text);
                newTaskInput.value = '';
                await renderTasks();
            }
        }
    });

    // Initial render
    renderTasks();
});