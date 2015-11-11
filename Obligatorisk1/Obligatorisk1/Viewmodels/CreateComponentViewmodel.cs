using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obligatorisk1.Models;

namespace Obligatorisk1.Viewmodels
{
    public class CreateComponentViewmodel
    {
        public Component Component { get; set; }
        public string SpecificComponentListAsJson { get; set; }
    }
}