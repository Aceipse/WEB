using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Obligatorisk1.Models
{
    public class Component
    {
        [Key]
        public int Id { get; set; }
        public List<SpecificComponent> SpecificComponent { get; set; }
        [Required]
        public string ComponentName { get; set; }
        public string ComponentInfo { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string Datasheet { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
        public string ManufacturerLink { get; set; }
        public List<ComponentComment> ComponentComments { get; set; }
    }
}