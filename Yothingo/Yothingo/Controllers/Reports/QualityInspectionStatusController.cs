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
    public class QualityInspectionStatusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QualityInspectionStatus
        public ActionResult Index()
        {
            return View(db.QualityInspectionStatus.ToList());
        }

        // GET: QualityInspectionStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualityInspectionStatus qualityInspectionStatus = db.QualityInspectionStatus.Find(id);
            if (qualityInspectionStatus == null)
            {
                return HttpNotFound();
            }
            return View(qualityInspectionStatus);
        }

        // GET: QualityInspectionStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QualityInspectionStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StatusId,StatusDecription")] QualityInspectionStatus qualityInspectionStatus)
        {
            if (ModelState.IsValid)
            {
                db.QualityInspectionStatus.Add(qualityInspectionStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(qualityInspectionStatus);
        }

        // GET: QualityInspectionStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualityInspectionStatus qualityInspectionStatus = db.QualityInspectionStatus.Find(id);
            if (qualityInspectionStatus == null)
            {
                return HttpNotFound();
            }
            return View(qualityInspectionStatus);
        }

        // POST: QualityInspectionStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StatusId,StatusDecription")] QualityInspectionStatus qualityInspectionStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(qualityInspectionStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(qualityInspectionStatus);
        }

        // GET: QualityInspectionStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QualityInspectionStatus qualityInspectionStatus = db.QualityInspectionStatus.Find(id);
            if (qualityInspectionStatus == null)
            {
                return HttpNotFound();
            }
            return View(qualityInspectionStatus);
        }

        // POST: QualityInspectionStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QualityInspectionStatus qualityInspectionStatus = db.QualityInspectionStatus.Find(id);
            db.QualityInspectionStatus.Remove(qualityInspectionStatus);
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
