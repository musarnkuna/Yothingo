using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace YothingoSprint1.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [DisplayName("Employment Date:")]
        public String JoinDate { get; set; }

        [DisplayName("Date Of Birth:")]
        public String DateOfBirth { get; set; }

        [DisplayName("First Name:")]
        public string FirstName { get; set; }

        [DisplayName("Last Name:")]
        public string LastName { get; set; }

        [DisplayName("Job Title:")]
        public string JobTitle { get; set; }

        [DisplayName("Department:")]
        public string Department { get; set; }

        [DisplayName("Email:")]
        public string Email { get; set; }

        [DisplayName("Gender:")]
        public string Gender { get; set; }

        [DisplayName("Salary:")]
        public string Salary { get; set; }

        public string ReportingTo { get; set; }
        [DisplayName("Reporting To:")]
        public string ReportingHead { get; set; }

        [DisplayName("Is Reporting Head:")]
        public Boolean IsReportingHead { get; set; }

        [DisplayName("Is Active:")]
        public Boolean IsActive { get; set; }

        [DisplayName("Employee Name:")]
        public string Fullname
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName);
            }
        }
        public virtual IList<Project> Projects { get; set; }

        //[ScaffoldColumn(false)]
        //public IList<Comment> Comments { get; set; }
    }
}
