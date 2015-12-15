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

var workoutController=function(req,res){
    var workout=mongoose.model('workoutProgram');
    
    workout.find(function(err,workouts){
       if (err) return console.error(err);
       res.render('workout', { Workouts: workouts });
    });

}

var specificWorkoutController=function(req,res){
    var workout=mongoose.model('workoutProgram');
    
    workout.findOne({'_id':req.params.workoutId},function(err,workout){
       if (err) return console.error(err);
       res.render('specificWorkout', { Workout: workout });
    });

}


router.get('/workout', workoutController);
router.get('/specificWorkout/:workoutId', specificWorkoutController);
router.get('/', homeController);

module.exports = router;
