using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;


namespace Obligatorisk2REST
{
    public class dbHandler : IdbHandler
    {
        private IMongoDatabase _db;

        public dbHandler()
        {
            var connection = ConfigurationManager.AppSettings["mongoString"];
            var client = new MongoClient(connection);
            _db = client.GetDatabase("MongoLab-qq");
        }

        public IMongoDatabase getDb()
        {
            return _db;
        }
    }
}