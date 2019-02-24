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
    public class PartController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Part/
        public ActionResult Index()
        {
            return View(db.Parts.ToList());
        }

        // GET: /Part/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part Part = db.Parts.Find(id);
            if (Part == null)
            {
                return HttpNotFound();
            }
            return View(Part);
        }

        // GET: /Part/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Part/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Name,Description,UnitPrice")] Part Part)
        {
            if (ModelState.IsValid)
            {
                db.Parts.Add(Part);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Part);
        }

        // GET: /Part/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part Part = db.Parts.Find(id);
            if (Part == null)
            {
                return HttpNotFound();
            }
            return View(Part);
        }

        // POST: /Part/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Name,Description,UnitPrice")] Part Part)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Part).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Part);
        }

        // GET: /Part/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Part Part = db.Parts.Find(id);
            if (Part == null)
            {
                return HttpNotFound();
            }
            return View(Part);
        }

        // POST: /Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Part Part = db.Parts.Find(id);
            db.Parts.Remove(Part);
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
