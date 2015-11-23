using System;
using System.Collections.Generic;

namespace Obligatorisk2REST.Models
{
    public class FoodCollection
    {
        public string Id { get; set; }
        public List<Food> Foods { get; set; }
        public DateTime Date { get; set; }
    }
}