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

namespace YothingoSprint1.Controllers.MeetingSchedule
{
    public class MinutesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Minutes
        public ActionResult Index()
        {
            var minutes = db.Minutes.Include(m => m.applicationUser).Include(m => m.Event);
            return View(minutes.ToList());
        }

        // GET: Minutes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Minutes minutes = db.Minutes.Find(id);
            if (minutes == null)
            {
                return HttpNotFound();
            }
            return View(minutes);
        }

        // GET: Minutes/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Users, "Id", "Email");
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Subject");
            return View();
        }

        // POST: Minutes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingMinutesId,Id,EventID,Attendees,Details,Date")] Minutes minutes)
        {
            if (ModelState.IsValid)
            {
                db.Minutes.Add(minutes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Users, "Id", "Email", minutes.Id);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Subject", minutes.EventID);
            return View(minutes);
        }

        // GET: Minutes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Minutes minutes = db.Minutes.Find(id);
            if (minutes == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", minutes.Id);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Subject", minutes.EventID);
            return View(minutes);
        }

        // POST: Minutes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingMinutesId,Id,EventID,Attendees,Details,Date")] Minutes minutes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(minutes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Users, "Id", "Email", minutes.Id);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "Subject", minutes.EventID);
            return View(minutes);
        }

        // GET: Minutes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Minutes minutes = db.Minutes.Find(id);
            if (minutes == null)
            {
                return HttpNotFound();
            }
            return View(minutes);
        }

        // POST: Minutes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Minutes minutes = db.Minutes.Find(id);
            db.Minutes.Remove(minutes);
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
