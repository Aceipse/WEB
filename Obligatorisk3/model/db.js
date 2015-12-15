var mongoose=require('mongoose');
mongoose.connect('mongodb://aaa:123@ds042128.mongolab.com:42128/MongoLab-qq')
var db=mongoose.connection;
db.on('error', console.error.bind(console, 'connection error:'));
db.once('open', function (callback) {

  var fitnessUserSchema= mongoose.Schema({
    name: String,
    log:  Array
  });
  var workoutProgramSchema=mongoose.Schema({
    name: String,
    exercises:  Array
  });
  var exerciseSchema=mongoose.Schema({
    name: String,
    description: String,
    numberOfSets: Number,
    repetitionsOrTime: String
  });
  
  
  var fitnessUser= mongoose.model('fitnessUser',fitnessUserSchema);
  var workoutProgram= mongoose.model('workoutProgram',workoutProgramSchema);
  var exercise= mongoose.model('exercise',exerciseSchema);
/*  
  var nikolaj=new fitnessUser({name:'Nikolaj',log:[]})
  nikolaj.save(function (err, fluffy) {
  if (err) return console.error(err);
});

var exercisea=new exercise({name:'squat',description:'very leg exercise',numberOfSets:100,repetitionsOrTime:'500 reps Brah'})


var woprogram=new workoutProgram({name:'Awesome workout',exercises:[exercisea]})
  woprogram.save(function (err, fluffy) {
  if (err) return console.error(err);
});
*/


});