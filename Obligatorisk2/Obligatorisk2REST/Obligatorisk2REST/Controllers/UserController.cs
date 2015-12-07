using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Obligatorisk2REST.Models;

namespace Obligatorisk2REST.Controllers
{
    public class UserController : ApiController
    {
        private readonly IMongoDatabase _db;

        public UserController()
        {
            _db =  new dbHandler().getDb();
        }
        // GET: api/User
        public string Get()
        {
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            return _db.GetCollection<User>("User").Find(_ => true).ToListAsync().Result.ToJson(jsonWriterSettings);
        }

        // GET: api/User/5
        public string Get(string id)
        {
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            return _db.GetCollection<User>("User").Find(x=> x.Id == id).SingleAsync().Result.ToJson(jsonWriterSettings);
        }

        // POST: api/User
        public string Post([FromBody]string value)
        {
            var user = BsonSerializer.Deserialize<User>(value);
            user.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            _db.GetCollection<User>("User").InsertOneAsync(user).Wait();
            var jsonWriterSettings = new JsonWriterSettings { OutputMode = JsonOutputMode.Strict };
            return user.ToJson(jsonWriterSettings);
        }

        // PATCH: api/User/5
        public void Patch(string id, [FromBody]string value)
        {

            User user;
            try
            {
                user = BsonSerializer.Deserialize<User>(value);
            }
            catch (Exception e)
            {
                
                throw;
            }

            //Order by date
            user.FoodCollections = user.FoodCollections.OrderBy(x => x.Date).ToList();
            _db.GetCollection<User>("User").FindOneAndReplaceAsync(x => x.Id == id,user).Wait();
        }

        // DELETE: api/User/5
        public void Delete(string id)
        {
            _db.GetCollection<User>("User").FindOneAndDeleteAsync(x => x.Id == id).Wait();
        }
    }
}
