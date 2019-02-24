using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YothingoSprint1.Models.Reports
{
    public class DailyReport
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int DailyReportId { get; set; }

        [DisplayName("Request Number")]
        public int RequestID { get; set; }

        public Request Request { get; set; }

        [DisplayName("Problem Description")]
        [MaxLength(500)]
        public string ReportDescription { get; set; }

        [DisplayName("Report Summary")]
        [Required(ErrorMessage = "Report Summary is required")]
        [MaxLength(500)]
        public string Summary { get; set; }

        [DisplayName("Date Created")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime CreateDate { get; set; }

    }
}