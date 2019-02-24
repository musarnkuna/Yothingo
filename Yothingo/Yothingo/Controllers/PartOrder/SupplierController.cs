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
    public class SupplierController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Supplier/
        public ActionResult Index()
        {
            if (db.Suppliers != null)
            {
                return View(db.Suppliers.ToList());
            }

            ViewBag.Message = "No Supplier";
            return View();
        }

        // GET: /Supplier/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Supplier = db.Suppliers.Find(id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            return View(Supplier);
        }

        // GET: /Supplier/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Supplier/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "ID,FirstName,LastName,Email,Name,Phone,Address")] Supplier Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Suppliers.Add(Supplier);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Supplier);
        }

        // GET: /Supplier/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Supplier = db.Suppliers.Find(id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            return View(Supplier);
        }

        // POST: /Supplier/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "ID,FirstName,LastName,Email,Name,Name,Phone,Address")] Supplier Supplier)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Supplier).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Supplier);
        }

        // GET: /Supplier/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier Supplier = db.Suppliers.Find(id);
            if (Supplier == null)
            {
                return HttpNotFound();
            }
            return View(Supplier);
        }

        // POST: /Supplier/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Supplier Supplier = db.Suppliers.Find(id);
            db.Suppliers.Remove(Supplier);
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
