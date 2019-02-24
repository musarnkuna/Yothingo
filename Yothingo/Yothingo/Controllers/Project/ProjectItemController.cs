using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YothingoSprint1.Models;

namespace YothingoSprint1
{
    public class ProjectItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /ProjectItem/
        public ActionResult Index()
        {
            var projectitems = db.ProjectItems.Include(i => i.Project).Include(i => i.Part).ToList();
            return View(projectitems);
        }

        // GET: /ProjectItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectItem projectitem = db.ProjectItems.Find(id);
            if (projectitem == null)
            {
                return HttpNotFound();
            }
            return View(projectitem);
        }

        // GET: /ProjectItem/Create
        public ActionResult Create()
        {
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Status");
            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name");
            return View();
        }

        // POST: /ProjectItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PartID,ProjectID,Quantity")] ProjectItem projectitem)
        {
            if (ModelState.IsValid)
            {
                db.ProjectItems.Add(projectitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Status", projectitem.ProjectID);
            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name", projectitem.PartID);
            return View(projectitem);
        }

        // GET: /ProjectItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectItem projectitem = db.ProjectItems.Find(id);
            if (projectitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Status", projectitem.ProjectID);
            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name", projectitem.PartID);
            return View(projectitem);
        }

        // POST: /ProjectItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PartID,ProjectID,Quantity")] ProjectItem projectitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(projectitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProjectID = new SelectList(db.Projects, "ID", "Status", projectitem.ProjectID);
            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name", projectitem.PartID);
            return View(projectitem);
        }

        // GET: /ProjectItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProjectItem projectitem = db.ProjectItems.Find(id);
            if (projectitem == null)
            {
                return HttpNotFound();
            }
            return View(projectitem);
        }

        // POST: /ProjectItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProjectItem projectitem = db.ProjectItems.Find(id);
            db.ProjectItems.Remove(projectitem);
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
    }
}
