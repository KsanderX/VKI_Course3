const http = require("http"); 
const server = http.createServer(async (req, res) => {
  if (req.method === "POST") {
    const buffers = [];
    for await (const chunk of req) {
      buffers.push(chunk);
    }
    const data = Buffer.concat(buffers).toString();
    console.log(data);
  }
  res.end();
})
server.listen(3000, "127.0.0.1", () => {
    const { address, port } = server.address();
    console.log(`Сервер запущен ${address}:${port}`);
});