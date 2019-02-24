using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Part
    {
        public int PartID { get; set; }

        [Required(ErrorMessage ="Part Name Required ")]
        [DisplayName("Part Name")]
        public string Name { get; set; }
    
        [Required(ErrorMessage = "Part Description Required ")]
        [StringLength(30, MinimumLength = 10, ErrorMessage = "Minimum 10 alphabetic characters")]
        public string Description { get; set; }

        [Required]
        [DisplayName("Price (each)")]
        public double UnitPrice { get; set; }
    }
}