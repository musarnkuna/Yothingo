//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.Entity;
//using System.IO;
//using System.Linq;
//using System.Net;
//using System.Net.Mail;
//using System.Threading.Tasks;
//using System.Web;
//using System.Web.Mvc;
//using YothingoSprint1.Models;

//namespace YothingoSprint1.Controllers
//{
//    public class UploadController : Controller
//    {
//        private ApplicationDbContext db = new ApplicationDbContext();

//        public ActionResult Index()
//        {
//            var upload = db.Uploads.ToList();
//            return View(upload);
//        }

//        // GET: /Invoice/Details/5
//        public ActionResult Details(int? id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }

//            Upload upload = db.Uploads.Find(id);
//            if (upload == null)
//            {
//                return HttpNotFound();
//            }
//            return View(upload);
//        }

//        //
//        // GET: /Upload/Create
//        public ActionResult Create()
//        {
//            #region find upload number as maxId + 1
//            int maxID = 0;
//            int uploadNumber = 0;

//            // Create a psuedo upload with the maximum value of ID + 1.
//            // Find the maximum value of id
//            //by default identity cloumn increment hoi
//            var uploadIDs = db.Uploads.Select(i => i.ID);
//            if (uploadIDs != null & uploadIDs.Count() > 0)
//            {
//                maxID = uploadIDs.Max();
//            }

//            // Upload Number is the highest ID already generated in the table + 1. So that the
//            // When this upload is inserted it automatically gets the next id
//            uploadNumber = maxID + 1;
//            #endregion

//            ViewBag.Suppliers = db.Suppliers.ToList();
//            ViewBag.Statuses = db.Statuses.ToList();
//            ViewBag.Parts = db.Parts.ToList();

//            // Create a upload with default settings
//            Upload upload = new Upload()
//                {

//                    ID = uploadNumber, // upload number will be stored to the ID
//                    UploadItems = new List<UploadItem>(),
//                    UploadDate = DateTime.Now
//                };


//            return View(upload);
//        }

//        public ActionResult AddNewUploadItemRow()
//        {
//            ViewBag.Design = db.Designs.ToList();
//            UploadItem uploadItem = new UploadItem() { Design = new Design() };
//            return PartialView("EditorTemplates/UploadItem", uploadItem);
//        }

//        // AJAX GET: /Upload/GetClientPartial/2 on Supplier select
       
//        public PartialViewResult GetClientPartial(int SupplierID)
//        {
//            Supplier Supplier = db.Suppliers.Find(SupplierID);

//            // Return a partial view the the data of the selected Supplier
//            return PartialView("EditorTemplates/Client", Supplier);
//        }

//        //public PartialViewResult GetStatusPartial(int StatusID)
//        //{
//        //    Status Status = db.Statuses.Find(StatusID);

//        //    // Return a partial view the the data of the selected Status
//        //    return PartialView("EditorTemplates/Status", Status);
//        //}


//        //Ajax POST /Upload/SelectProduct
//        public PartialViewResult SelectProduct(int DesignID, int? index)
//        {
//            Design Design = db.Designs.Find(DesignID);

//            UploadItem uploadItem = new UploadItem() { Design = Design, DesignID = Design.DesignId};
//            ViewBag.Designs = db.Designs.ToList();
//            return PartialView("EditorTemplates/UploadItem", uploadItem);
//        }

//        // POST: /Upload/Create
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        public ActionResult Create(/*[Bind(Include = "ID,SupplierID,StatusID,UploadDate,DueDate,SalesTaxPercent,PaymentID,AmountDue,Status")]*/ Upload upload)
//        {
//            if (ModelState.IsValid)
//            {
//                foreach (var item in upload.UploadItems)
//                {
                   
//                    // First find the Part
//                    item.Design = db.Designs.Find(item.DesignID);
//                    item.UploadID = upload.ID;
//                }

//                #region Saving the upload Model

//                // Save the upload Model
//                db.Uploads.Add(upload);
//                db.SaveChanges();
//                #endregion

//                return RedirectToAction("Index");
//            }
//            else
//            {
//                // Debug errors
//                var modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);

//                ViewBag.ErrorMessage = modelStateErrors.ToString();
//            }

//            return View(upload);
//        }


//        public ActionResult Edit(int? id)
//        {
//            ViewBag.Suppliers = db.Suppliers.ToList();
//            ViewBag.Statuses = db.Statuses.ToList();
//            ViewBag.Parts = db.Parts.ToList();
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Upload upload = db.Uploads.Find(id);
//            if (upload == null)
//            {
//                return HttpNotFound();
//            }
//            return View(upload);
//        }

//        // POST: /Test/Edit/5
//        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
//        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult Edit([Bind(Include = "ID,SupplierID,StatusID,UploadsDate")] Upload upload, string newPayment)
//        {
//            // Find the upload from db
//            upload = db.Uploads.Find(upload.ID);

//            try
//            {
//                // Save
//                db.Entry(upload).State = EntityState.Modified;
//                db.SaveChanges();
//                return RedirectToAction("Index");
//            }
//            catch (Exception e)
//            {

//                Console.WriteLine(e);
//            }
//            return View(upload);
//        }

//        //
//        // GET: /Design/Delete/5
//        public ActionResult Delete(int id)
//        {
//            if (id == null)
//            {
//                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
//            }
//            Design design = db.Uploads.Find(id);
//            if (design == null)
//            {
//                return HttpNotFound();
//            }
//            return View(invoice);
//        }

//        // POST: /Part/Delete/5
//        [HttpPost, ActionName("Delete")]
//        [ValidateAntiForgeryToken]
//        public ActionResult DeleteConfirmed(int id)
//        {
//            Invoice invoice = db.Uploads.Find(id);

//            db.Uploads.Remove(invoice);
//            db.SaveChanges();
//            return RedirectToAction("Index");
//        }
       
//        public ActionResult Email(int? id)
//        {
//            Invoice invoice  =db.Uploads.Find(id);
           

//            return View(invoice);
//        }
//        [HttpPost]
       
//        public ActionResult Email(int? id, string email)
//        {
//            Invoice invoice = new Invoice();

//            if (!String.IsNullOrEmpty(email)||id!=null)
//            {
//               invoice = db.Uploads.Find(id);

//                Supplier Supplier = db.Suppliers.Find(invoice.SupplierID);

//                using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTPuser"], email))
//                {
//                    mm.Subject = "Invoice Statement";

//                    string body = string.Empty;
//                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Invoice/EmailTemp.html")))
//                    {

//                        body = reader.ReadToEnd();

//                    }

//                    body = body.Replace("{id}", invoice.ID.ToString());
//                    body = body.Replace("{name}", Supplier.Name);


//                    var model = db.InvoiceItems.Where(x=>x.InvoiceID==invoice.ID).ToList();

//                    string tbody = "";
//                    foreach(var item in model)
//                    {
//                        string table = "<tr>"+"<td>{prodID}</td>"+"<td>{item}</td>"+"<td>{quant}</td>"+"</tr>";
//                        table= table.Replace("{prodID}", item.PartID.ToString());
//                        table= table.Replace("{item}", item.Part.Name.ToString());
//                        table=  table.Replace("{quant}", item.Quantity.ToString());

//                        tbody += table;

//                    }
                    
//                    body = body.Replace("{tbody}",tbody);
            
//                    mm.Body = body;
//                    mm.IsBodyHtml = true;
//                    SmtpClient smtp = new SmtpClient();
//                    smtp.Host = ConfigurationManager.AppSettings["Host"];
//                    smtp.EnableSsl = true;

//                    NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"], ConfigurationManager.AppSettings["SMTPpassword"]);
//                    smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
//                    smtp.Credentials = NetworkCred;
//                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
//                    smtp.Send(mm);

//                    return RedirectToAction("Confirm");

//                }
//            }
//            invoice = db.Uploads.Find(id);
//            return View(invoice);
//        }
//        public ActionResult Confirm()
//        {
//            return View();

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
