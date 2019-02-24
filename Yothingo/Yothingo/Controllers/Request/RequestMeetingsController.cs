using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YothingoSprint1.Models;

namespace YothingoSprint1.Controllers.MeetingSchedule
{
    public class RequestMeetingsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RequestMeetings
        [Authorize]
        [Authorize]
        public ActionResult Index(int? id)
        {
            var g = User.Identity.GetUserId();
            var requests = db.RequestMeetings.Include(r => r.applicationUser).ToList().Where(x => x.UserId == g);
            if (db.RequestMeetings != null)
            {
                return View(db.RequestMeetings.ToList());
            }

            ViewBag.Message = "No Meeting Requested";

            return View(db.RequestMeetings.ToList());
        }

        public ActionResult IndexAdmin()
        {
            return View(db.RequestMeetings.ToList());
        }

        // GET: RequestMeetings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestMeeting requestMeeting = db.RequestMeetings.Find(id);
            if (requestMeeting == null)
            {
                return HttpNotFound();
            }
            return View(requestMeeting);
        }

        // GET: RequestMeetings/Create
        [Authorize]
        public ActionResult Create()
        {
            ViewBag.UserId = new SelectList(db.Users, "Id", "Email,Description,Name,Address");
            Request model = new Request();
            model.UserId = HttpContext.User.Identity.Name;


            RequestMeeting requestMeeting = new RequestMeeting()
            {
                RequestStatus = "None",
                Date = DateTime.Now
            };
            return View(requestMeeting);
        }

        // POST: RequestMeetings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MeetingRequestId,UserId,Agenda,Description,Date,Telephone,RequestStatus")] RequestMeeting requestMeeting)
        {
            var us = db.Users.ToList().Where(x => x.Email == HttpContext.User.Identity.Name).FirstOrDefault().Id;
            requestMeeting.UserId = us;
            if (ModelState.IsValid)
            {
                requestMeeting.UserId = requestMeeting.UserId;

                db.RequestMeetings.Add(requestMeeting);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.Users, "Id", "Email", requestMeeting.UserId);
            return View(requestMeeting);
        }

        // GET: RequestMeetings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestMeeting requestMeeting = db.RequestMeetings.Find(id);
            if (requestMeeting == null)
            {
                return HttpNotFound();
            }
            return View(requestMeeting);
        }

        // POST: RequestMeetings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MeetingRequestId,Agenda,Description,Date,Telephone")] RequestMeeting requestMeeting)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestMeeting).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(requestMeeting);
        }

        // GET: RequestMeetings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestMeeting requestMeeting = db.RequestMeetings.Find(id);
            if (requestMeeting == null)
            {
                return HttpNotFound();
            }
            return View(requestMeeting);
        }

        // POST: RequestMeetings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestMeeting requestMeeting = db.RequestMeetings.Find(id);
            db.RequestMeetings.Remove(requestMeeting);
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
