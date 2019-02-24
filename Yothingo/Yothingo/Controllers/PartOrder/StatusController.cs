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
    public class StatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Status/
        public ActionResult Index()
        {
            if (db.Statuses != null)
            {
                return View(db.Statuses.ToList());
            }

            ViewBag.Message = "No Status";
            return View();
        }

        // GET: /Status/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status Status = db.Statuses.Find(id);
            if (Status == null)
            {
                return HttpNotFound();
            }
            return View(Status);
        }

        // GET: /Status/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Status/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "ID,Name,StatusDescription")] Status Status)
        {
            if (ModelState.IsValid)
            {
                db.Statuses.Add(Status);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Status);
        }

        // GET: /Status/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status Status = db.Statuses.Find(id);
            if (Status == null)
            {
                return HttpNotFound();
            }
            return View(Status);
        }

        // POST: /Status/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "ID,Name,StatusDescription")] Status Status)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Status).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Status);
        }

        // GET: /Status/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Status Status = db.Statuses.Find(id);
            if (Status == null)
            {
                return HttpNotFound();
            }
            return View(Status);
        }

        // POST: /Status/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Status Status = db.Statuses.Find(id);
            db.Statuses.Remove(Status);
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
