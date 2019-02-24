using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using YothingoSprint1.Models;

namespace YothingoSprint1.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var invoices = db.Invoices.ToList();
            return View(invoices);
        }

        // GET: /Invoice/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        //
        // GET: /Invoice/Create
        public ActionResult Create()
        {
            #region find invoice number as maxId + 1
            int maxID = 0;
            int invoiceNumber = 0;

            // Create a psuedo invoice with the maximum value of ID + 1.
            // Find the maximum value of id
            //by default identity cloumn increment hoi
            var invoiceIDs = db.Invoices.Select(i => i.ID);
            if (invoiceIDs != null & invoiceIDs.Count() > 0)
            {
                maxID = invoiceIDs.Max();
            }

            // Invoice Number is the highest ID already generated in the table + 1. So that the
            // When this invoice is inserted it automatically gets the next id
            invoiceNumber = maxID + 1;
            #endregion

            ViewBag.Suppliers = db.Suppliers.ToList();
            ViewBag.Statuses = db.Statuses.ToList();
            ViewBag.Parts = db.Parts.ToList();

            // Create a Invoice with default settings
            Invoice invoice = new Invoice()
                {

                    ID = invoiceNumber, // invoice number will be stored to the ID
                    InvoiceItems = new List<InvoiceItem>(),
                    InvoiceDate = DateTime.Now
                };


            return View(invoice);
        }

        public ActionResult AddNewInvoiceItemRow()
        {
            ViewBag.Parts = db.Parts.ToList();
            InvoiceItem invoiceItem = new InvoiceItem() { Part = new Part(), Quantity = 1 };
            return PartialView("EditorTemplates/InvoiceItem", invoiceItem);
        }

        // AJAX GET: /Invoice/GetClientPartial/2 on Supplier select
       
        public PartialViewResult GetClientPartial(int SupplierID)
        {
            Supplier Supplier = db.Suppliers.Find(SupplierID);

            // Return a partial view the the data of the selected Supplier
            return PartialView("EditorTemplates/Client", Supplier);
        }

        //public PartialViewResult GetStatusPartial(int StatusID)
        //{
        //    Status Status = db.Statuses.Find(StatusID);

        //    // Return a partial view the the data of the selected Status
        //    return PartialView("EditorTemplates/Status", Status);
        //}


        //Ajax POST /Invoice/SelectProduct
        public PartialViewResult SelectProduct(int PartID, int? index)
        {
            Part Part = db.Parts.Find(PartID);

            InvoiceItem invoiceItem = new InvoiceItem() { Part = Part, PartID = Part.PartID, Quantity = 1 };
            ViewBag.Parts = db.Parts.ToList();
            return PartialView("EditorTemplates/InvoiceItem", invoiceItem);
        }

        // POST: /Invoice/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(/*[Bind(Include = "ID,SupplierID,StatusID,InvoiceDate,DueDate,SalesTaxPercent,PaymentID,AmountDue,Status")]*/ Invoice invoice)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in invoice.InvoiceItems)
                {
                   
                    // First find the Part
                    item.Part = db.Parts.Find(item.PartID);
                    item.InvoiceID = invoice.ID;
                }

                #region Saving the invoice Model

                // Save the invoice Model
                db.Invoices.Add(invoice);
                db.SaveChanges();
                #endregion

                return RedirectToAction("Index");
            }
            else
            {
                // Debug errors
                var modelStateErrors = this.ModelState.Values.SelectMany(m => m.Errors);

                ViewBag.ErrorMessage = modelStateErrors.ToString();
            }

            return View(invoice);
        }


        public ActionResult Edit(int? id)
        {
            ViewBag.Suppliers = db.Suppliers.ToList();
            ViewBag.Statuses = db.Statuses.ToList();
            ViewBag.Parts = db.Parts.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "Name", invoice.SupplierID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", invoice.StatusID);
            return View(invoice);
        }

        // POST: /Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,SupplierID,StatusID,InvoiceDate")] Invoice invoice, string newPayment)
        {
            // Find the invoice from db
            invoice = db.Invoices.Find(invoice.ID);

            try
            {
                // Save
                db.Entry(invoice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            ViewBag.SupplierID = new SelectList(db.Suppliers, "ID", "Name", invoice.SupplierID);

            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", invoice.StatusID);
            return View(invoice);
        }

        //
        // GET: /Invoice/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Invoice invoice = db.Invoices.Find(id);
            if (invoice == null)
            {
                return HttpNotFound();
            }
            return View(invoice);
        }

        // POST: /Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Invoice invoice = db.Invoices.Find(id);

            db.Invoices.Remove(invoice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
       
        //public ActionResult Email(int? id)
        //{
        //    Invoice invoice  =db.Invoices.Find(id);
           

        //    return View(invoice);
        //}
        //[HttpPost]
       
        public ActionResult Email(int? id)
        {
            Invoice invoice = new Invoice();
             invoice = db.Invoices.Find(id);

            Supplier Supplier = db.Suppliers.Find(invoice.SupplierID);
            string email = Supplier.Email;

            if (!String.IsNullOrEmpty(email)||id!=null)
            {

                using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTPuser"], email))
                {
                    mm.Subject = "Invoice Statement";

                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Invoice/EmailTemp.html")))
                    {

                        body = reader.ReadToEnd();

                    }

                    body = body.Replace("{id}", invoice.ID.ToString());
                    body = body.Replace("{name}", Supplier.Name);


                    var model = db.InvoiceItems.Where(x=>x.InvoiceID==invoice.ID).ToList();

                    string tbody = "";
                    foreach(var item in model)
                    {
                        string table = "<tr>"+"<td>{prodID}</td>"+"<td>{item}</td>"+"<td>{quant}</td>"+ "<td>{totalit}</td>" + "</tr>";
                        table= table.Replace("{prodID}", item.PartID.ToString());
                        table= table.Replace("{item}", item.Part.Name.ToString());
                        table=  table.Replace("{quant}", item.Quantity.ToString());
                        table = table.Replace("{totalit}", item.Quantity.ToString());
                        tbody += table;

                    }
                    
                    body = body.Replace("{tbody}",tbody);
            
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = ConfigurationManager.AppSettings["Host"];
                    smtp.EnableSsl = true;

                    NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"], ConfigurationManager.AppSettings["SMTPpassword"]);
                    smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
                    smtp.Credentials = NetworkCred;
                    smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
                    smtp.Send(mm);
                    TempData["Success"] = "Email sent to "+Supplier.Name;
                    return RedirectToAction("Index");


                }
            }
            invoice = db.Invoices.Find(id);
            return View("Index");
        }
        public ActionResult Confirm()
        {
            return View();

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
