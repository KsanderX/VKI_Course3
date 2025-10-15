const express = require('express');
const app = express();
const port = process.env.PORT || 3001;
const cors = require(`cors`);

app.use(express.json());

app.use(express.urlencoded({extended: true}))

app.use(
    cors({
        credentials: true,
        origin : true
    })
);

app.get('/', (req, res) => {   
    res.send('Hello World!');   
});

app.post('/', function (req, res, next) {
    console.log(req.body);
    res.json(req.body);
})

app.listen(port, () => {
    console.log(`Example app listening on port ${port}!`)
});