﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Obligatorisk1.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Category Name")]
        public string Value { get; set; }
    }
}