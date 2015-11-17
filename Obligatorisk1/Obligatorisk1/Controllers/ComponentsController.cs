using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Microsoft.Ajax.Utilities;
using Obligatorisk1.Models;
using Obligatorisk1.Viewmodels;

namespace Obligatorisk1.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ComponentsController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        public ComponentsController()
        {
        }
        public ComponentsController(DatabaseContext dbContext)
        {
            db = dbContext;
        }
        // GET: Components
        public ActionResult Index(string category, string search)
        {
            ViewBag.Categories = db.Categories.AsNoTracking().ToList().OrderBy(x=>x.Value);
            if (category.IsNullOrWhiteSpace() && search.IsNullOrWhiteSpace())
            {
                return View(db.Components.Include(x => x.Category).Include("SpecificComponent.LoanInformation").ToList());
            }
            if (!category.IsNullOrWhiteSpace())
            {
                return View(db.Components.Include(x => x.Category).Include("SpecificComponent.LoanInformation").Where(x => x.Category.Value == category).ToList());
            }
            if (!search.IsNullOrWhiteSpace())
            {
                return View(db.Components.Include(x => x.Category).Include("SpecificComponent.LoanInformation").Where(x => x.ComponentName.Contains(search) || x.ComponentInfo.Contains(search)).ToList());
            }
            return HttpNotFound();
        }

        // GET: Components/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Include(x => x.Category).Include("SpecificComponent.LoanInformation.User").FirstOrDefault(x=>x.Id==id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // GET: Components/Create
        public ActionResult Create()
        {
            ViewBag.Categories = db.Categories.AsNoTracking().ToList();
            return View();
        }

        // POST: Components/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateComponentViewmodel componentVm)
        {

            if (ModelState.IsValid)
            {
                componentVm.Component.SpecificComponent = new List<SpecificComponent>();
                if(componentVm.SpecificComponentListAsJson!=null)
                    componentVm.Component.SpecificComponent = new JavaScriptSerializer().Deserialize<List<SpecificComponent>>(componentVm.SpecificComponentListAsJson);

                if (componentVm.Image != null)
                {
                    componentVm.Component.ImageMimeType = componentVm.Image.ContentType;
                    componentVm.Component.Image = new byte[componentVm.Image.ContentLength];
                    componentVm.Image.InputStream.Read(componentVm.Component.Image, 0, componentVm.Image.ContentLength);
                }
                db.Components.Add(componentVm.Component);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(componentVm);
        }

        // GET: Components/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Include(x => x.SpecificComponent).First(x => x.Id == id);
            if (component == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories.AsNoTracking().ToList();

            return View(new CreateComponentViewmodel(component));
        }

        // POST: Components/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CreateComponentViewmodel componentVm)
        {

            if (ModelState.IsValid)
            {
                if (componentVm.Image != null)
                {
                    componentVm.Component.ImageMimeType = componentVm.Image.ContentType;
                    componentVm.Component.Image = new byte[componentVm.Image.ContentLength];
                    componentVm.Image.InputStream.Read(componentVm.Component.Image, 0, componentVm.Image.ContentLength);
                }
                db.Entry(componentVm.Component).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(componentVm);
        }

        // GET: Components/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Find(id);
            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }

        // POST: Components/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Component component = db.Components.Include(x => x.SpecificComponent).Include(x => x.Category).FirstOrDefault(x => x.Id == id);
            db.Components.Remove(component);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Lend(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Component component = db.Components.Include("SpecificComponent.LoanInformation").Include("SpecificComponent.LoanInformation.User").First(x => x.Id == id);

            if (component == null)
            {
                return HttpNotFound();
            }
            return View(component);
        }
        public FileContentResult GetImage(int componentId)
        {
            Component component = db.Components.FirstOrDefault(x => x.Id == componentId);
            if (component != null)
            {
                return File(component.Image, component.ImageMimeType);
            }
            else
            {
                return null;
            }
        }

        public ActionResult EditLoanInformation(int componentId, int specificId, LoanInformation loanInformation)
        {
            if (ModelState.IsValid)
            {
                Component component = db.Components.Include(x => x.SpecificComponent).Include("SpecificComponent.LoanInformation.User").First(x => x.Id == componentId);
                SpecificComponent spComp = component.SpecificComponent.First(x => x.Id == specificId);

                if (loanInformation.User == null)
                {
                    int UserId = spComp.LoanInformation.User.Id;
                    db.LoanInformations.Remove(spComp.LoanInformation);
                    db.Users.Remove(db.Users.First(x => x.Id == UserId));
                }
                else
                {
                    spComp.LoanInformation = loanInformation;
                }

                db.Entry(component).State = EntityState.Modified;

                db.SaveChanges();
            }
            return RedirectToAction("Lend", new { id = componentId });
        }
    }
}
