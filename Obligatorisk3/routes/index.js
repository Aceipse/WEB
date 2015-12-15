var express = require('express');
var mongoose= require('mongoose');
var router = express.Router();

var homeController=function(req,res){
    var fitnessUser=mongoose.model('fitnessUser');
    
    fitnessUser.find(function(err,fitnessUsers){
       if (err) return console.error(err);
      
       res.render('index', { Users: fitnessUsers });
    });
//     var nikolaj=new fitnessUser({name:'aaaaaaj',log:[]})
//   nikolaj.save(function (err, fluffy) {
//   if (err) return console.error(err);
// });
   // res.render('index', { title: 'Express' });
}
/* GET home page. */
router.get('/', homeController);
router.post('/User/:name', function(req, res) {
    var fitnessUser=mongoose.model('fitnessUser');
     var user=new fitnessUser({name:req.params.name,log:[]})
     user.save(function (err, obj){ 
        
     });
     res.end("yes");
});

module.exports = router;
