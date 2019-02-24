using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YothingoSprint1.Models;
using System.IO;

namespace YothingoSprint1.Controllers
{
    public class DesignController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Designs
        public ActionResult Index()
        {
            var Designs = db.Designs.Include(d => d.Request);
            return View(db.Designs.ToList());
        }

        public ActionResult IndexCustomer(int id)
        {
            ApplicationUser u = new ApplicationUser();

            Request c = db.Requests.Find(id);
            var Designs = db.Designs.ToList().Where(x => x.ID == c.ID);
            return View(Designs);
        }
        public ActionResult Display()
        {
            var result = db.Designs.ToList();
            return View(result);

        }

        // GET: Designs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Design design = db.Designs.Find(id);
            if (design == null)
            {
                return HttpNotFound();
            }
            return View(design);
        }

        // GET: Designs/Create
        public ActionResult Create()
        {
            ViewBag.ID = new SelectList(db.Requests, "ID", "Request_Number");
            return View();
        }

        // POST: Designs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DesignId,ID,Name,Image,ImageType")] Design design, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                design.ImageType = Path.GetExtension(file.FileName);
                design.Image = ConvertToBytes(file);
            }
            if (ModelState.IsValid)
            {
                db.Designs.Add(design);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Requests, "ID", "Request_Number");
            return View(design);
        }
        private byte[] ConvertToBytes(HttpPostedFileBase file)
        {
            BinaryReader reader = new BinaryReader(file.InputStream);
            return reader.ReadBytes((int)file.ContentLength);
        }

        // Display File
        public FileStreamResult RenderImage(int id)
        {
            MemoryStream ms = null;

            var item = db.Designs.FirstOrDefault(x => x.DesignId == id);
            if (item != null)
            {
                ms = new MemoryStream(item.Image);
            }
            return new FileStreamResult(ms, item.ImageType);
        }


        // GET: Designs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Design design = db.Designs.Find(id);
            if (design == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.Requests, "ID", "Request_Number");
            return View(design);
        }

        // POST: Designs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DesignId,Name,Image,ImageType")] Design design, HttpPostedFileBase file)
        {
            if (file != null && file.ContentLength > 0)
            {
                design.ImageType = Path.GetExtension(file.FileName);
                design.Image = ConvertToBytes(file);
            }
            if (ModelState.IsValid)
            {
                db.Entry(design).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID = new SelectList(db.Requests, "ID", "Request_Number");
            return View(design);
        }

        // GET: Designs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Design design = db.Designs.Find(id);
            if (design == null)
            {
                return HttpNotFound();
            }
            return View(design);
        }

        // POST: Designs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Design design = db.Designs.Find(id);
            db.Designs.Remove(design);
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
