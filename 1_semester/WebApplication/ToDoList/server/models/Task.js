class Task {
  constructor(id, text, done, date) {
    this.id = id;
    this.text = text;
    this.done = done === 1; 
    this.date = date;
  }
}

module.exports = Task;