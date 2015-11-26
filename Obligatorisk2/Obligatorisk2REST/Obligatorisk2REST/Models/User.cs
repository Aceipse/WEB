using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson.Serialization.Attributes;

namespace Obligatorisk2REST.Models
{
    public class User
    {
        [BsonId]
        public string Id { get; set; }
        public string Name { get; set; }
        public decimal Weight { get; set; }
        public string HealthState { get; set; }
        public List<FoodCollection> FoodCollections { get; set; }
    }
}