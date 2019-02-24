using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Project
    {

        [DisplayName("Project Number")]
        public int ID { get; set; }


        [Required (ErrorMessage ="Select Employee")]
        public int EmployeeID { get; set; }

        [Required(ErrorMessage = "Select Status")]
        public int ProjectStatusID { get; set; }

        [Required(ErrorMessage ="Select Start Date")]
        [DisplayName("Project Start Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Select End Date")]
        [DisplayName("Project End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        //[Required(ErrorMessage ="Status is required")]
        // public string Status { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Status Statuses { get; set; }
        public virtual IList<ProjectItem> ProjectItems { get; set; }

    }
}