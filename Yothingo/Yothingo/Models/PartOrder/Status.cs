using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Status
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Status Name is required")]
        [DisplayName("Status Name")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Status Description is required")]
        [DisplayName("Status Description")]
        [StringLength(30)]
        public string StatusDescription { get; set; }

        public virtual IList<Invoice> Invoices { get; set; }
        public virtual IList<Quotation> Quotations { get; set; }
    }
}