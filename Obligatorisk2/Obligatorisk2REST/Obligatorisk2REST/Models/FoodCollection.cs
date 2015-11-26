using System;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace Obligatorisk2REST.Models
{
    public class FoodCollection
    {
        [BsonId]
        public string Id { get; set; }
        public List<Food> Foods { get; set; }
        public DateTime Date { get; set; }
    }
}