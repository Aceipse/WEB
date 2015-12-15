var express = require('express');
var mongoose= require('mongoose');
var router = express.Router();

var homeController=function(req,res){
    var fitnessUser=mongoose.model('fitnessUser');
    
    fitnessUser.find(function(err,fitnessUsers){
       if (err) return console.error(err);
      
       res.render('index', { Users: fitnessUsers });
    });
}

var workoutController=function(req,res){
    var workout=mongoose.model('workoutProgram');
    var fitnessUser=mongoose.model('fitnessUser');
    
    var userFound;
    
    fitnessUser.findOne({'_id':req.params.userId},function(err,user){
       if (err) return console.error(err);
       userFound = user;
    });
    
    workout.find(function(err,workouts){
       if (err) return console.error(err);
       res.render('workout', { Workouts: workouts, User: userFound });
    });

}

var specificWorkoutController=function(req,res){
    var workout=mongoose.model('workoutProgram');
    
    workout.findOne({'_id':req.params.workoutId},function(err,workout){
       if (err) return console.error(err);
       res.render('specificWorkout', { Workout: workout });
    });

}

var userController = function(req, res) {
    var fitnessUser=mongoose.model('fitnessUser');
     var user=new fitnessUser({name:req.params.name,log:[]})
     user.save(function (err, obj){ 
        res.end("yes");
     });
}

var addWorkoutController = function(req,res){
    var workoutProgram=mongoose.model('workoutProgram');
     var woProgram=new workoutProgram({name:req.params.workoutName,exercises:[]})
     woProgram.save(function (err, obj){ 
        res.end("yes");
     });
}

var deleteWorkoutController = function(req,res){
    var workout=mongoose.model('workoutProgram');
    
    workout.findByIdAndRemove({'_id':req.params.workoutId},function(err,workout){
       if (err) return console.error(err);
       ;
    });
}

router.get('/workout/:userId', workoutController);
router.get('/specificWorkout/:workoutId', specificWorkoutController);
router.get('/', homeController);
router.post('/workout/:workoutName',addWorkoutController)
router.post('/User/:name', userController);
router.delete('/workout/:workoutId', deleteWorkoutController)

module.exports = router;
