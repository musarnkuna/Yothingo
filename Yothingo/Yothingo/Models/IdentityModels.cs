using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace YothingoSprint1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer<ApplicationDbContext>(new CreateDatabaseIfNotExists<ApplicationDbContext>());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Supplier> Suppliers { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Status> Statuses { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Invoice> Invoices { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.InvoiceItem> InvoiceItems { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Part> Parts { get; set; }

        //public System.Data.Entity.DbSet<YothingoSprint1.Models.ApplicationUser> applicationUsers { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Request> Requests { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Quotation> Quotations { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.QuotationItem> QuotationItems { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Design> Designs { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Event> Events { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Room> Rooms { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Upload> Uploads { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.UploadItem> UploadItems { get; set; }

        //Project Tables
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Project> Projects { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.ProjectItem> ProjectItems { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.Employee> Employees { get; set; }
        public System.Data.Entity.DbSet<YothingoSprint1.Models.ProjectStatus> ProjectStatuses { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Reports.DailyReport> DailyReports { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Reports.QualityInspectionStatus> QualityInspectionStatus { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Reports.Minutes> Minutes { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.Reports.QualityInspection> QualityInspections { get; set; }

        public System.Data.Entity.DbSet<YothingoSprint1.Models.RequestMeeting> RequestMeetings { get; set; }
        public DbSet<IdentityUserRole> UserInRole { get; set; }

    }
}
