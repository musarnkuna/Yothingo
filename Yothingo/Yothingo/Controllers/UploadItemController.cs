//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.Entity;
//using System.Linq;
//using System.Net;
//using System.Web;
//using System.Web.Mvc;
//using YothingoSprint1.Models;

//namespace YothingoSprint1
//{
//    public class UploadItemController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        // GET: /InvoiceItem/
//        public ActionResult Index()
//        {
//            var uploaditems = db.UploadItems.Include(i => i.Invoice).Include(i => i.Part).ToList();
//            return View(uploaditems);
//        }

//        // GET: /InvoiceItem/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            InvoiceItem invoiceitem = db.InvoiceItems.Find(id);
//            if (invoiceitem == null)
//            {
//                return HttpNotFound();
//            }
//            return View(invoiceitem);
//        }

//        // GET: /InvoiceItem/Create
//        public ActionResult Create()
//        {
//            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Status");
//            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name");
//            return View();
//        }

//        // POST: /InvoiceItem/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Create([Bind(Include="ID,PartID,InvoiceID,Quantity")] InvoiceItem invoiceitem)
//        {
//            if (ModelState.IsValid)
//            {
//                db.InvoiceItems.Add(invoiceitem);
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }

//            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Status", invoiceitem.InvoiceID);
//            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name", invoiceitem.PartID);
//            return View(invoiceitem);
//        }

//        // GET: /InvoiceItem/Edit/5
//        public ActionResult Edit(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            InvoiceItem invoiceitem = db.InvoiceItems.Find(id);
//            if (invoiceitem == null)
//            {
//                return HttpNotFound();
//            }
//            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Status", invoiceitem.InvoiceID);
//            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name", invoiceitem.PartID);
//            return View(invoiceitem);
//        }

//        // POST: /InvoiceItem/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include="ID,PartID,InvoiceID,Quantity")] InvoiceItem invoiceitem)
//        {
//            if (ModelState.IsValid)
//            {
//                db.Entry(invoiceitem).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            ViewBag.InvoiceID = new SelectList(db.Invoices, "ID", "Status", invoiceitem.InvoiceID);
//            ViewBag.PartID = new SelectList(db.Parts, "ID", "Name", invoiceitem.PartID);
//            return View(invoiceitem);
//        }

//        // GET: /InvoiceItem/Delete/5
//        public ActionResult Delete(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            InvoiceItem invoiceitem = db.InvoiceItems.Find(id);
//            if (invoiceitem == null)
//            {
//                return HttpNotFound();
//            }
//            return View(invoiceitem);
//        }

//        // POST: /InvoiceItem/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            InvoiceItem invoiceitem = db.InvoiceItems.Find(id);
//            db.InvoiceItems.Remove(invoiceitem);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }

//        protected override void Dispose(bool disposing)
//        {
//            if (disposing)
//            {
//                db.Dispose();
//            }
//            base.Dispose(disposing);
//        }
//    }
//}
