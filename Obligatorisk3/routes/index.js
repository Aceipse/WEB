var express = require('express');
var mongoose= require('mongoose');
var router = express.Router();

var homeController=function(req,res){
    var fitnessUser=mongoose.model('fitnessUser');
    var nikolaj=new fitnessUser({name:'aaaaaaj',log:[]})
  nikolaj.save(function (err, fluffy) {
  if (err) return console.error(err);
});
    res.render('index', { title: 'Express' });
}
/* GET home page. */
router.get('/', homeController);

module.exports = router;
