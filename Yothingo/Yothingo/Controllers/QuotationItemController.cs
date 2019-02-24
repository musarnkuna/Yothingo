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
    public class QuotationItemController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /QuotationItem/
        public ActionResult Index()
        {
            var quotationitems = db.QuotationItems.Include(i => i.Quotation).Include(i => i.Part).ToList();
            return View(quotationitems);
        }

        // GET: /QuotationItem/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationItem quotationitem = db.QuotationItems.Find(id);
            if (quotationitem == null)
            {
                return HttpNotFound();
            }
            return View(quotationitem);
        }

        // GET: /QuotationItem/Create
        public ActionResult Create()
        {
            ViewBag.QuotationID = new SelectList(db.Quotations, "ID", "Status");
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Name");
            return View();
        }

        // POST: /QuotationItem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,PartID,QuotationID,Quantity,Taxable,Total")] QuotationItem quotationitem)
        {
            if (ModelState.IsValid)
            {
                db.QuotationItems.Add(quotationitem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.QuotationID = new SelectList(db.Quotations, "ID", "Status", quotationitem.QuotationID);
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Name", quotationitem.PartID);
            return View(quotationitem);
        }

        // GET: /QuotationItem/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationItem quotationitem = db.QuotationItems.Find(id);
            if (quotationitem == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuotationID = new SelectList(db.Quotations, "ID", "Status", quotationitem.QuotationID);
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Name", quotationitem.PartID);
            return View(quotationitem);
        }

        // POST: /QuotationItem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,PartID,QuotationID,Quantity,Taxable,Total")] QuotationItem quotationitem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(quotationitem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuotationID = new SelectList(db.Quotations, "ID", "Status", quotationitem.QuotationID);
            ViewBag.PartID = new SelectList(db.Parts, "PartID", "Name", quotationitem.PartID);
            return View(quotationitem);
        }

        // GET: /QuotationItem/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuotationItem quotationitem = db.QuotationItems.Find(id);
            if (quotationitem == null)
            {
                return HttpNotFound();
            }
            return View(quotationitem);
        }

        // POST: /QuotationItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuotationItem quotationitem = db.QuotationItems.Find(id);
            db.QuotationItems.Remove(quotationitem);
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
