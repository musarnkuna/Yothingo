using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YothingoSprint1.Models;
using YothingoSprint1.Models.Reports;

namespace YothingoSprint1.Controllers.Reports
{
    public class QualityInspectionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QualityInspections
        public ActionResult Index()
        {
            var qualityInspections = db.QualityInspections.Include(q => q.QualityInspectionStatus).Include(q => q.Request);
            return View(qualityInspections.ToList());
        }

        // GET: QualityInspections/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualityInspection qualityInspection = db.QualityInspections.Find(id);
            if (qualityInspection == null)
            {
                return HttpNotFound();
            }
            return View(qualityInspection);
        }

        // GET: QualityInspections/Create
        public ActionResult Create()
        {
            ViewBag.StatusId = new SelectList(db.QualityInspectionStatus, "StatusId", "StatusDecription");
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number");
            return View();
        }

        // POST: QualityInspections/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QualityInspectionId,RequestID,ReportDescription,Summary,StatusId,CreateDate")] QualityInspection qualityInspection)
        {
            if (ModelState.IsValid)
            {
                db.QualityInspections.Add(qualityInspection);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StatusId = new SelectList(db.QualityInspectionStatus, "StatusId", "StatusDecription", qualityInspection.StatusId);
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", qualityInspection.RequestID);
            return View(qualityInspection);
        }

        // GET: QualityInspections/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualityInspection qualityInspection = db.QualityInspections.Find(id);
            if (qualityInspection == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusId = new SelectList(db.QualityInspectionStatus, "StatusId", "StatusDecription", qualityInspection.StatusId);
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", qualityInspection.RequestID);
            return View(qualityInspection);
        }

        // POST: QualityInspections/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QualityInspectionId,RequestID,ReportDescription,Summary,StatusId,CreateDate")] QualityInspection qualityInspection)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qualityInspection).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusId = new SelectList(db.QualityInspectionStatus, "StatusId", "StatusDecription", qualityInspection.StatusId);
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", qualityInspection.RequestID);
            return View(qualityInspection);
        }

        // GET: QualityInspections/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualityInspection qualityInspection = db.QualityInspections.Find(id);
            if (qualityInspection == null)
            {
                return HttpNotFound();
            }
            return View(qualityInspection);
        }

        // POST: QualityInspections/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QualityInspection qualityInspection = db.QualityInspections.Find(id);
            db.QualityInspections.Remove(qualityInspection);
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
