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
        [Display(Name = "Specific components")]
        public List<SpecificComponent> SpecificComponent { get; set; }
        [Required]
        [Display(Name = "Component name")]
        public string ComponentName { get; set; }
        [Display(Name = "Component Info")]
        public string ComponentInfo { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        [DataType(DataType.Url, ErrorMessage = "Datasheet url is invalid")]
        public string Datasheet { get; set; }
        public byte[] Image { get; set; }
        public string ImageMimeType { get; set; }
        [Display(Name = "Manufacturer link")]
        [DataType(DataType.Url, ErrorMessage = "Manufacturer url is invalid")]
        public string ManufacturerLink { get; set; }
        public List<ComponentComment> ComponentComments { get; set; }
    }
}