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

namespace Obligatorisk1.Controllers
{
    public class ApplicationUsersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ApplicationUsers
        public ActionResult Index()
        {
            HttpContext.GetOwinContext()
                .Get<ApplicationSignInManager>()
                .UserManager.AddToRole(HttpContext.GetOwinContext().Get<ApplicationSignInManager>().UserManager.Users.ToList()[0].Id, "Admin");
            return View(HttpContext.GetOwinContext().Get<ApplicationSignInManager>().UserManager.Users.ToList());
        }

       
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
