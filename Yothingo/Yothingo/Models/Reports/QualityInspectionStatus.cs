using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YothingoSprint1.Models.Reports
{
    public class QualityInspectionStatus
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int StatusId { get; set; }

        [Required]
        [DisplayName("Status")]
        public string StatusDecription { get; set; }
    }
}