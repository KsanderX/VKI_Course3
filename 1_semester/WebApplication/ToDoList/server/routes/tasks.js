const express = require("express");
const db = require("../db/db");
const Task = require("../models/Task");

const router = express.Router();

router.get("/", (req, res) => {
  db.all("SELECT * FROM tasks", [], (err, rows) => {
    if (err) return res.status(500).json({ error: err.message });
    res.json(rows.map(r => new Task(r.id, r.text, r.done, r.date)));
  });
});

router.post("/", (req, res) => {
  const { text } = req.body;
  const date = new Date().toLocaleDateString();

  db.run(
    "INSERT INTO tasks (text, done, date) VALUES (?, ?, ?)",
    [text, 0, date],
    function (err) {
      if (err) return res.status(500).json({ error: err.message });
      res.json(new Task(this.lastID, text, 0, date));
    }
  );
});

router.put("/:id", (req, res) => {
  const { text, done } = req.body;
  db.run(
    "UPDATE tasks SET text = ?, done = ? WHERE id = ?",
    [text, done ? 1 : 0, req.params.id],
    function (err) {
      if (err) return res.status(500).json({ error: err.message });
      res.json({ updated: this.changes });
    }
  );
});

router.delete("/:id", (req, res) => {
  db.run("DELETE FROM tasks WHERE id = ?", req.params.id, function (err) {
    if (err) return res.status(500).json({ error: err.message });
    res.json({ deleted: this.changes });
  });
});

router.delete("/", (req, res) => {
  db.run("DELETE FROM tasks", function (err) {
    if (err) return res.status(500).json({ error: err.message });
    res.json({ deleted: this.changes });
  });
});

module.exports = router;