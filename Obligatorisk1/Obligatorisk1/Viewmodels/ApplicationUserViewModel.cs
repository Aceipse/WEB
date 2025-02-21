﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Obligatorisk1.Models;

namespace Obligatorisk1.Viewmodels
{
    public class ApplicationUserViewModel
    {
        public ApplicationUserViewModel(bool isAdmin, ApplicationUser user)
        {
            IsAdmin = isAdmin;
            User = user;
        }
        public ApplicationUser User { get; set; }
        [Display(Name = "Is Admin")]
        public bool IsAdmin { get; set; }
    }
}