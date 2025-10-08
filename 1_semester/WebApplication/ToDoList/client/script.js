document.addEventListener('DOMContentLoaded', () => {
    const taskList = document.getElementById('task-list');
    const newTaskInput = document.getElementById('new-task');
    const addTaskButton = document.getElementById('add-task');
    const clearAllButton = document.getElementById('clear-all');
    const filterLinks = document.querySelectorAll('.filter-link');
    const sortSelect = document.getElementById('sort-tasks');

    let tasks = [];
    let currentFilter = 'all';
    let currentSort = 'default';
    const API_URL = "http://localhost:3000/api/tasks";

    async function fetchTasks() {
        const res = await fetch(API_URL);
        tasks = await res.json();
        renderTasks();
    }

    function renderTasks() {
        taskList.innerHTML = '';
        let filtered = tasks.filter(task => {
            if (currentFilter === 'done') return task.done;
            if (currentFilter === 'not-done') return !task.done;
            return true;
        });

        if (currentSort === 'name') {
            filtered.sort((a, b) => a.text.localeCompare(b.text));
        } else if (currentSort === 'date') {
            filtered.sort((a, b) => new Date(a.date) - new Date(b.date));
        }

        filtered.forEach(task => {
            const li = document.createElement('li');
            li.innerHTML = `
                <input type="checkbox" ${task.done ? 'checked' : ''} data-id="${task.id}">
                <span contenteditable="true" 
                      class="editable ${task.done ? 'done' : ''}" 
                      data-id="${task.id}">
                      ${task.text}
                </span>
                <sub>${task.date}</sub>
                <button class="delete-btn" data-id="${task.id}">❌</button>
            `;
            taskList.appendChild(li);
        });
    }

    async function addTask(text) {
        const res = await fetch(API_URL, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ text })
        });
        const newTask = await res.json();
        tasks.push(newTask);
        renderTasks();
    }

    async function deleteTask(id) {
        await fetch(`${API_URL}/${id}`, { method: "DELETE" });
        tasks = tasks.filter(t => t.id != id);
        renderTasks();
    }

    async function clearAllTasks() {
        await fetch(API_URL, { method: "DELETE" });
        tasks = [];
        renderTasks();
    }

    async function toggleTaskDone(id) {
        const task = tasks.find(t => t.id == id);
        task.done = !task.done;
        await fetch(`${API_URL}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(task)
        });
        renderTasks();
    }

    async function updateTaskText(id, newText) {
        const task = tasks.find(t => t.id == id);
        task.text = newText;
        await fetch(`${API_URL}/${id}`, {
            method: "PUT",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(task)
        });
        renderTasks();
    }


    addTaskButton.addEventListener('click', () => {
        const text = newTaskInput.value.trim();
        if (text) {
            addTask(text);
            newTaskInput.value = '';
        }
    });

    clearAllButton.addEventListener('click', clearAllTasks);

    // чекбокс → done / not done
    taskList.addEventListener('change', (e) => {
        if (e.target.type === 'checkbox') {
            const id = e.target.dataset.id;
            toggleTaskDone(id);
        }
    });

    // потеря фокуса → сохранить новый текст
    taskList.addEventListener('blur', (e) => {
        if (e.target.classList.contains('editable')) {
            const id = e.target.dataset.id;
            updateTaskText(id, e.target.textContent.trim());
        }
    }, true);

    taskList.addEventListener('click', (e) => {
        if (e.target.classList.contains('delete-btn')) {
            const id = e.target.dataset.id;
            deleteTask(id);
        }
    });

    filterLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();
            currentFilter = e.target.dataset.filter;
            filterLinks.forEach(l => l.classList.remove('active'));
            e.target.classList.add('active');
            renderTasks();
        });
    });

    sortSelect.addEventListener('change', (e) => {
        currentSort = e.target.value;
        renderTasks();
    });

    fetchTasks();
});
