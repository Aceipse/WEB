using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
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
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/User/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/User
        public void Post([FromBody]string value)
        {
            
            _db.GetCollection<User>("User").InsertOneAsync(new User() {Name = "Martin"}).Wait();
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
        }
    }
}
