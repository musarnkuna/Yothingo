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
    public class ProjectController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            var projects = db.Projects.ToList();
            return View(projects);
        }

        // GET: /Project/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        //
        // GET: /Project/Create
        public ActionResult Create()
        {
            #region find project number as maxId + 1
            int maxID = 0;
            int projectNumber = 0;

            // Create a psuedo project with the maximum value of ID + 1.
            // Find the maximum value of id
            //by default identity cloumn increment hoi
            var projectIDs = db.Projects.Select(i => i.ID);
            if (projectIDs != null & projectIDs.Count() > 0)
            {
                maxID = projectIDs.Max();
            }

            // Project Number is the highest ID already generated in the table + 1. So that the
            // When this project is inserted it automatically gets the next id
            projectNumber = maxID + 1;
            #endregion

            ViewBag.Employees = db.Employees.ToList();
            ViewBag.ProjectStatuses = db.ProjectStatuses.ToList();
            ViewBag.Parts = db.Parts.ToList();

            // Create a Project with default settings
            Project project = new Project()
                {

                    ID = projectNumber, // project number will be stored to the ID
                    ProjectItems = new List<ProjectItem>(),
                    StartDate = DateTime.Now
                };


            return View(project);
        }

        public ActionResult AddNewProjectItemRow()
        {
            ViewBag.Parts = db.Parts.ToList();
            ProjectItem projectItem = new ProjectItem() { Part = new Part(), Quantity = 1 };
            return PartialView("EditorTemplates/ProjectItem", projectItem);
        }

        // AJAX GET: /Project/GetClientPartial/2 on Employee select
       
        public PartialViewResult GetClientPartial(int EmployeeID)
        {
            Employee Employee = db.Employees.Find(EmployeeID);

            // Return a partial view the the data of the selected Employee
            return PartialView("EditorTemplates/Client", Employee);
        }

        //public PartialViewResult GetStatusPartial(int StatusID)
        //{
        //    Status Status = db.Statuses.Find(StatusID);

        //    // Return a partial view the the data of the selected Status
        //    return PartialView("EditorTemplates/Status", Status);
        //}


        //Ajax POST /Project/SelectProduct
        public PartialViewResult SelectProduct(int PartID, int? index)
        {
            Part Part = db.Parts.Find(PartID);

            ProjectItem projectItem = new ProjectItem() { Part = Part, PartID = Part.PartID, Quantity = 1 };
            ViewBag.Parts = db.Parts.ToList();
            return PartialView("EditorTemplates/ProjectItem", projectItem);
        }

        // POST: /Project/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create(/*[Bind(Include = "ID,EmployeeID,StatusID,ProjectDate,DueDate,SalesTaxPercent,PaymentID,AmountDue,Status")]*/ Project project)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in project.ProjectItems)
                {
                   
                    // First find the Part
                    item.Part = db.Parts.Find(item.PartID);
                    item.ProjectID = project.ID;
                }

                #region Saving the project Model

                // Save the project Model
                db.Projects.Add(project);
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

            return View(project);
        }


        public ActionResult Edit(int? id)
        {
            ViewBag.Employees = db.Employees.ToList();
            ViewBag.ProjectStatuses = db.ProjectStatuses.ToList();
            ViewBag.Parts = db.Parts.ToList();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name", project.EmployeeID);
            ViewBag.ProjectStatusID = new SelectList(db.ProjectStatuses, "ID", "Name", project.ProjectStatusID);
            return View(project);
        }

        // POST: /Test/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,EmployeeID,ProjectStatusID,StartDate,EndDate")] Project project, string newPayment)
        {
            // Find the project from db
            project = db.Projects.Find(project.ID);

            try
            {
                // Save
                db.Entry(project).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {

                Console.WriteLine(e);
            }

            ViewBag.EmployeeID = new SelectList(db.Employees, "ID", "Name", project.EmployeeID);

            ViewBag.ProjectStatusID = new SelectList(db.ProjectStatuses, "ID", "Name", project.ProjectStatusID);
            return View(project);
        }

        //
        // GET: /Project/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = db.Projects.Find(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: /Part/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = db.Projects.Find(id);

            db.Projects.Remove(project);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        #region email
        //public ActionResult Email(int? id)
        //{
        //    Project project  =db.Projects.Find(id);
           

        //    return View(project);
        //}
        //[HttpPost]
       
        //public ActionResult Email(int? id, string email)
        //{
        //    Project project = new Project();

        //    if (!String.IsNullOrEmpty(email)||id!=null)
        //    {
        //       project = db.Projects.Find(id);

        //        Employee Employee = db.Employees.Find(project.EmployeeID);

        //        using (MailMessage mm = new MailMessage(ConfigurationManager.AppSettings["SMTPuser"], email))
        //        {
        //            mm.Subject = "Project Statement";

        //            string body = string.Empty;
        //            using (StreamReader reader = new StreamReader(Server.MapPath("~/Views/Project/EmailTemp.html")))
        //            {

        //                body = reader.ReadToEnd();

        //            }

        //            body = body.Replace("{id}", project.ID.ToString());
        //            body = body.Replace("{name}", Supplier.Name);


        //            var model = db.ProjectItems.Where(x=>x.ProjectID==project.ID).ToList();

        //            string tbody = "";
        //            foreach(var item in model)
        //            {
        //                string table = "<tr>"+"<td>{prodID}</td>"+"<td>{item}</td>"+"<td>{quant}</td>"+"</tr>";
        //                table= table.Replace("{prodID}", item.PartID.ToString());
        //                table= table.Replace("{item}", item.Part.Name.ToString());
        //                table=  table.Replace("{quant}", item.Quantity.ToString());

        //                tbody += table;

        //            }
                    
        //            body = body.Replace("{tbody}",tbody);
            
        //            mm.Body = body;
        //            mm.IsBodyHtml = true;
        //            SmtpClient smtp = new SmtpClient();
        //            smtp.Host = ConfigurationManager.AppSettings["Host"];
        //            smtp.EnableSsl = true;

        //            NetworkCredential NetworkCred = new NetworkCredential(ConfigurationManager.AppSettings["SMTPuser"], ConfigurationManager.AppSettings["SMTPpassword"]);
        //            smtp.UseDefaultCredentials = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
        //            smtp.Credentials = NetworkCred;
        //            smtp.Port = int.Parse(ConfigurationManager.AppSettings["Port"]);
        //            smtp.Send(mm);

        //            return RedirectToAction("Confirm");

        //        }
        //    }
        //    project = db.Projects.Find(id);
        //    return View(project);
        //}
        //public ActionResult Confirm()
        //{
        //    return View();

        //}
        #endregion
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
