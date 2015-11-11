using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Obligatorisk1.Models;
using Obligatorisk1.Viewmodels;

namespace Obligatorisk1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ApplicationUsersController : Controller
    {
        // GET: ApplicationUsers
        public ActionResult Index()
        {
            List<ApplicationUserViewModel> list = new List<ApplicationUserViewModel>();

            foreach (var user in HttpContext.GetOwinContext().Get<ApplicationSignInManager>().UserManager.Users.ToList())
            {
                if (HttpContext.GetOwinContext().Get<ApplicationSignInManager>().UserManager.IsInRole(user.Id,"Admin"))
                {
                    list.Add(new ApplicationUserViewModel(true,user));
                }
                else
                {
                    list.Add(new ApplicationUserViewModel(false, user));
                }
            }
            return View(list);
        }

        public ActionResult MakeAdmin(string id)
        {
            HttpContext.GetOwinContext()
               .Get<ApplicationSignInManager>()
               .UserManager.AddToRole(id, "Admin");

            return RedirectToAction("Index");
        }
    }
}
