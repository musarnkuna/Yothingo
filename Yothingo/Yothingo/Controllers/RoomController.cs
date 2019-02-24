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
    public class RoomController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Room/
        public ActionResult Index()
        {
            if (db.Rooms != null)
            {
                return View(db.Rooms.ToList());
            }

            ViewBag.Message = "No Fridge";
            return View();
        }

        // GET: /Room/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room Room = db.Rooms.Find(id);
            if (Room == null)
            {
                return HttpNotFound();
            }
            return View(Room);
        }

        // GET: /Room/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Room/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include= "ID,Name,FridgeDescription")] Room Room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(Room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Room);
        }

        // GET: /Room/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room Room = db.Rooms.Find(id);
            if (Room == null)
            {
                return HttpNotFound();
            }
            return View(Room);
        }

        // POST: /Room/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include= "ID,Name,FridgeDescription")] Room Room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(Room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Room);
        }

        // GET: /Room/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room Room = db.Rooms.Find(id);
            if (Room == null)
            {
                return HttpNotFound();
            }
            return View(Room);
        }

        // POST: /Room/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room Room = db.Rooms.Find(id);
            db.Rooms.Remove(Room);
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
