const express = require("express");
const cors = require("cors");
const bodyParser = require("body-parser");

const tasksRoutes = require("./routes/tasks");

const app = express();
const PORT = 3000;

app.use(cors());
app.use(bodyParser.json());

app.use("/api/tasks", tasksRoutes);

app.listen(PORT, () => {
  console.log(` Сервер запущен: http://localhost:${PORT}`);
});