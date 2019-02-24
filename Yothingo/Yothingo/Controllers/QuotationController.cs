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
    public class QuotationController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index(int? id)
        {
            var Quotations = db.Quotations.ToList();
            return View(Quotations);
        }

        public ActionResult IndexCustomer(int id)
        {
            ApplicationUser u = new ApplicationUser();

            Request c = db.Requests.Find(id);
            var Quotations = db.Quotations.ToList().Where(x => x.RequestID == c.ID);
            return View(Quotations);
        }

        // GET: /Quotation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            //    Session["QuotationID"] = quotation.QuotationID;

            //    return RedirectToAction("PayFast", "payment", quotation);
            return View(quotation);
        }
        public ActionResult Payment(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            Session["QuotationID"] = quotation.QuotationID;

            return RedirectToAction("PayFast", "payment", quotation);

        }
        //
        // GET: /Quotation/Create
        public ActionResult Create()
        {
            #region find Quotation number as maxId + 1
            int maxID = 0;
            int quotationNumber = 0;

            // Create a psuedo Quotation with the maximum value of ID + 1.
            // Find the maximum value of id
            //by default identity cloumn increment hoi
            var quotationIDs = db.Quotations.Select(i => i.QuotationID);
            if (quotationIDs != null & quotationIDs.Count() > 0)
            {
                maxID = quotationIDs.Max();
            }

            // Quotation Number is the highest ID already generated in the table + 1. So that the
            // When this Quotation is inserted it automatically gets the next id
            quotationNumber = maxID + 1;
            #endregion

            ViewBag.Requests = db.Requests.ToList();
            ViewBag.Statuses = db.Statuses.ToList();
            ViewBag.Parts = db.Parts.ToList();

            // Create a Quotation with default settings
            Quotation quotation = new Quotation()
            {

                QuotationID = quotationNumber, // Quotation number will be stored to the ID
                QuotationItems = new List<QuotationItem>(),
                QuotationDate = DateTime.Now,
                SalesTaxPercent = 15
            };


            return View(quotation);
        }

        public ActionResult AddNewQuotationItemRow()
        {
            ViewBag.Parts = db.Parts.ToList();
            QuotationItem quotationItem = new QuotationItem() { Part = new Part(), Quantity = 1 };
            return PartialView("EditorTemplates/QuotationItem", quotationItem);
        }

        // AJAX GET: /Quotation/GetClientPartial/2 on Supplier select

        public PartialViewResult GetClientPartial(int RequestID)
        {
            Request Request = db.Requests.Find(RequestID);

            // Return a partial view the the data of the selected Request
            return PartialView("EditorTemplates/Client", Request);
        }
        public PartialViewResult GetStatusPartial(int StatusID)
        {
            Status Status = db.Statuses.Find(StatusID);

            // Return a partial view the the data of the selected Status
            return PartialView("EditorTemplates/Status", Status);
        }

        //Ajax POST /Quotation/SelectProduct
        public PartialViewResult SelectProduct(int PartID, int? index)
        {
            Part Part = db.Parts.Find(PartID);

            QuotationItem QuotationItem = new QuotationItem() { Part = Part, PartID = Part.PartID, Quantity = 1 };
            ViewBag.Parts = db.Parts.ToList();
            return PartialView("EditorTemplates/QuotationItem", QuotationItem);
        }

        // POST: /Quotation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(/*TODO BIND after validation [Bind(Include = "ID,SupplierID,QuotationDate,DueDate,SalesTaxPercent,PaymentID,AmountDue,Status")] */Quotation quotation)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in quotation.QuotationItems)
                {

                    // First find the Part
                    item.Part = db.Parts.Find(item.PartID);
                    item.QuotationID = quotation.QuotationID;
                }

                #region Saving the Quotation Mode
                // Save the Quotation Model
                db.Quotations.Add(quotation);
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

            return View(quotation);
        }


        public ActionResult Edit(int? id)
        {
            ViewBag.Requests = db.Requests.ToList();
            ViewBag.Statuses = db.Statuses.ToList();
            ViewBag.Parts = db.Parts.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", quotation.RequestID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", quotation.StatusID);
            return View(quotation);
        }

        // POST: /Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Quotation quotation, string newPayment)
        {
            // Find the Quotation from db
            quotation = db.Quotations.Find(quotation.QuotationID);

            try
            {
                // Save
                db.Entry(quotation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            ViewBag.RequestID = new SelectList(db.Requests, "ID", "Request_Number", quotation.RequestID);
            ViewBag.StatusID = new SelectList(db.Statuses, "ID", "Name", quotation.StatusID);
            return View(quotation);
        }

        //
        // GET: /Quotation/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Quotation quotation = db.Quotations.Find(id);
            if (quotation == null)
            {
                return HttpNotFound();
            }
            return View(quotation);
        }

        // POST: /Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Quotation quotation = db.Quotations.Find(id);

            db.Quotations.Remove(quotation);
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
        //public ActionResult Email(int? id)
        //{
        //    Quotation quotation = db.Quotations.Find(id);


        //    return View(quotation);
        //}
        // [HttpPost]

        public ActionResult Email(int? id)
        {
            Request request = new Request();
            Quotation quotation = new Quotation();

            string email = User.Identity.GetUserName();

            ApplicationUser u = new ApplicationUser();

            u.Email = email;

            if (!String.IsNullOrEmpty(email) || id != null)
            {
                quotation = db.Quotations.Find(id);
                request = db.Requests.Find(id);

                using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTPuser"], email))
                {
                    mm.Subject = "Quotation Statement";

                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Quotation/EmailTemp.html")))
                    {

                        body = reader.ReadToEnd();

                    }

                    body = body.Replace("{id}", request.Request_Number);
                    body = body.Replace("{sub}", quotation.TotalSalesTax.ToString());
                    body = body.Replace("{grandtot}", quotation.GrandTotal.ToString());
                    body = body.Replace("{date}", quotation.QuotationDate.ToString());

                    var model = db.QuotationItems.Where(x => x.QuotationID == quotation.QuotationID).ToList();

                    string tbody = "";
                    foreach (var item in model)
                    {
                        string table = "<tr>" + "<td>{prodID}</td>" + "<td>{item}</td>" + "<td>{quant}</td>" + "<td>{totalit}</td>" + "</tr>";
                        table = table.Replace("{prodID}", item.PartID.ToString());
                        table = table.Replace("{item}", item.Part.Name.ToString());
                        table = table.Replace("{quant}", item.Quantity.ToString());
                        table = table.Replace("{totalit}", item.Total.ToString());

                        tbody += table;

                    }


                    body = body.Replace("{tbody}", tbody);

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
                    TempData["Success"] = "Email Sent";
                    return RedirectToAction("Index");

                }
            }
            //invoice = db.Invoices.Find(id);
            return View("Index");
        }
    }
}
