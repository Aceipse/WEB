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
       workout.find(function(err,workouts){
        if (err) return console.error(err);
        res.render('workout', { Workouts: workouts, User: userFound });
    });
    });
    
   

}

var specificWorkoutController=function(req,res){
    var workout=mongoose.model('workoutProgram');
    
    workout.findOne({'_id':req.params.workoutId},function(err,workout){
       if (err) return console.error(err);
       res.render('specificWorkout', { Workout: workout,User:req.params.userId });
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
var addExerciseController = function(req,res){
    var workoutProgram=mongoose.model('workoutProgram');
    workoutProgram.findOne({'_id':req.params.workoutId},function(err,workout){
        var exerciseModel=mongoose.model('exercise');
        var exerciseToAdd=new exerciseModel({name:req.params.exerciseName,description:req.params.exerciseDescription,numberOfSets:req.params.numberOfSets,repetitionsOrTime:req.params.repsOrTime})
       workout.exercises.push(exerciseToAdd)
       workout.save(function (err, obj){ 
        res.end("yes");
     });
    });
}

var deleteWorkoutController = function(req,res){
    var workout=mongoose.model('workoutProgram');
    
    workout.findByIdAndRemove({'_id':req.params.workoutId},function(err,workout){
       if (err) return console.error(err);
        res.end("yes");
    });
}

var updateUserController = function(req,res){
    //console.log(req.params.state)
    if(req.params.state!='false')
    {
         var workoutProgram=mongoose.model('workoutProgram');
         var fitnessUser=mongoose.model('fitnessUser');
         
         workoutProgram.findOne({'_id':req.params.workoutId},function(err,workout){
         if (err) return console.error(err);
         var specificWorkout = workout._id;
         fitnessUser.findOne({'_id':req.params.userId},function(err,user){
         if (err) return console.error(err);
         user.log.push(specificWorkout);
         user.save();
         res.end("yes");
         });
         
         });
    }
    else
    {
        var fitnessUser=mongoose.model('fitnessUser');
         fitnessUser.findOne({'_id':req.params.userId},function(err,user){
         if (err) return console.error(err);
         for(var i in user.log){
          if(user.log[i]==req.params.workoutId){
          user.log.splice(i,1);
          break;
          }
         }
         user.save();
         res.end("yes");
         });
    }
}

router.get('/workout/:userId', workoutController);
router.get('/specificWorkout/:workoutId/:userId', specificWorkoutController);
router.get('/', homeController);
router.post('/workout/:workoutName',addWorkoutController);
router.post('/specificWorkout/:workoutId/:exerciseName/:exerciseDescription/:numberOfSets/:repsOrTime',addExerciseController);
router.post('/User/:name', userController);
router.post('/User/:userId/:workoutId/:state',updateUserController);
router.delete('/workout/:workoutId', deleteWorkoutController);

module.exports = router;
