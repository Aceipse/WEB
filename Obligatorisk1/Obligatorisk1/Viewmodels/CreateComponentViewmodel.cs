﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Obligatorisk1.Models;

namespace Obligatorisk1.Viewmodels
{
    public class CreateComponentViewmodel
    {
        public CreateComponentViewmodel()
        {
        }
        public CreateComponentViewmodel(Component component)
        {
            Component = component;
        }

        public Component Component { get; set; }
        public string SpecificComponentListAsJson { get; set; }
        public HttpPostedFileBase Image{ get; set; }
    }
}