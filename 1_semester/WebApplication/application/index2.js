const { promises } = require("fs"),
    http = require("http");

http.createServer(async (req, res) => {
    try {
        const data = await promises.readFile(__dirname + "\\index.js" , "utf8");
        res.writeHead(200);
        res.end(data);
    } catch (err) {
        res.writeHead(404);
        res.end(JSON.stringify(err));
    }
}).listen(3000);