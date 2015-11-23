using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Web.Http;
using MongoDB.Bson;
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
            return _db.GetCollection<User>("User").Find(_ => true).ToListAsync().Result.ToJson();
        }

        // GET: api/User/5
        public string Get(string id)
        {
            return _db.GetCollection<User>("User").Find(x=> x.Id == id).SingleAsync().Result.ToJson();
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
            var user = BsonSerializer.Deserialize<User>(value);
            user.Id = ObjectId.GenerateNewId(DateTime.Now).ToString();
            _db.GetCollection<User>("User").InsertOneAsync(user).Wait();
        }

        // PUT: api/User/5
        public void Put(string id, [FromBody]string value)
        {
            var user = BsonSerializer.Deserialize<User>(value);
            _db.GetCollection<User>("User").FindOneAndReplaceAsync(x => x.Id == id,user).Wait();
        }

        // DELETE: api/User/5
        public void Delete(string id)
        {
            _db.GetCollection<User>("User").FindOneAndDeleteAsync(x => x.Id == id).Wait();
        }
    }
}
