using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Obligatorisk2REST.Models;

namespace Obligatorisk2REST.Controllers
{
    public class FoodController : ApiController
    {
        private readonly IMongoDatabase _db;

        public FoodController()
        {
            _db = new dbHandler().getDb();
        }
        // GET: api/Food
        public string Get()
        {
            return _db.GetCollection<Food>("Food").Find(_ => true).ToListAsync().Result.ToJson();
        }

        // GET: api/Food/5
        public string Get(string id)
        {
            return _db.GetCollection<Food>("Food").Find(x => x.Id == id).SingleAsync().Result.ToJson();
        }

        // POST: api/Food
        public void Post([FromBody]string value)
        {
            var food = BsonSerializer.Deserialize<Food>(value);
            food.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            _db.GetCollection<Food>("Food").InsertOneAsync(food).Wait();
        }

        // PUT: api/Food/5
        public void Put(string id, [FromBody]string value)
        {
            var food = BsonSerializer.Deserialize<Food>(value);
            _db.GetCollection<Food>("Food").FindOneAndReplaceAsync(x => x.Id == id, food).Wait();
        }

        // DELETE: api/Food/5
        public void Delete(string id)
        {
            _db.GetCollection<Food>("Food").FindOneAndDeleteAsync(x => x.Id == id).Wait();
        }
    }
}
