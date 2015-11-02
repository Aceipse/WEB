using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Obligatorisk1.Models
{
    public class Component
    {
        [Key]
        public int Id { get; set; }
        public SpecificComponent SpecificComponent { get; set; }
        public string ComponentName { get; set; }
        public string ComponentInfo { get; set; }
        public Category Category { get; set; }
        public string Datasheet { get; set; }
        public string Image { get; set; }
        public string ManufacturerLink { get; set; }
        public string AdminComment { get; set; }
        public string UserComment { get; set; }
    }
}