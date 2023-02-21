const express = require('express');
const router = express.Router();
const pool = require('../database/maria.js');

// get /petever  : 저장된 강아지 정보 조회
router.get('/getDogInfo', async function(req, res){
    try {
      let query = 'SELECT dog_name, breed FROM Dog';
      const [rows] = await pool.query(query);
      res.status(200).send({result : rows, message : 'ok'});
    }
    catch(err) {
      res.status(500).send({result : [], message : 'selecting error : '+err});
    }
  });

module.exports = router;
