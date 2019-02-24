using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YothingoSprint1.Models.Reports
{
    public class QualityInspection
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int QualityInspectionId { get; set; }
        //public string Id { get; set; }
        //public ApplicationUser applicationUser { get; set; }
        public int RequestID { get; set; }

        public Request Request { get; set; }

        [DisplayName("Problem Description")]
        [MaxLength(500)]
        public string ReportDescription { get; set; }

        [DisplayName("Report Summary")]
        [Required(ErrorMessage = "Report Summary is required")]
        [MaxLength(500)]
        public string Summary { get; set; }

        [DisplayName("Status")]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public QualityInspectionStatus QualityInspectionStatus { get; set; }

        [DisplayName("Date Created")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }
    }

}
