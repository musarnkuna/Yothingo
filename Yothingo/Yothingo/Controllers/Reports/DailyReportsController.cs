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
    public class DailyReportsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: DailyReports
        public ActionResult Index()
        {
            var dailyReports = db.DailyReports.Include(d => d.Request);
            return View(dailyReports.ToList());
        }

        // GET: DailyReports/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = db.DailyReports.Find(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            return View(dailyReport);
        }

        // GET: DailyReports/Create
        public ActionResult Create()
        {
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number");
            return View();
        }

        // POST: DailyReports/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DailyReportId,RequestID,ReportDescription,Summary,CreateDate")] DailyReport dailyReport)
        {
            if (ModelState.IsValid)
            {
                db.DailyReports.Add(dailyReport);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", dailyReport.RequestID);
            return View(dailyReport);
        }

        // GET: DailyReports/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = db.DailyReports.Find(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", dailyReport.RequestID);
            return View(dailyReport);
        }

        // POST: DailyReports/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DailyReportId,RequestID,ReportDescription,Summary,CreateDate")] DailyReport dailyReport)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyReport).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", dailyReport.RequestID);
            return View(dailyReport);
        }

        // GET: DailyReports/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyReport dailyReport = db.DailyReports.Find(id);
            if (dailyReport == null)
            {
                return HttpNotFound();
            }
            return View(dailyReport);
        }

        // POST: DailyReports/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyReport dailyReport = db.DailyReports.Find(id);
            db.DailyReports.Remove(dailyReport);
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
