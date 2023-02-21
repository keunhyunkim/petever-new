const mysql = require("mysql2/promise");
const dbConfig = {
  host: "192.168.137.2",
  port: 3306,
  user: "petever",
  password: "petever",
  database: "petever",
  connectionLimit: 23,
};

const db_pool = mysql.createPool(dbConfig);
module.exports = db_pool;
