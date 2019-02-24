using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Invoice
    {

        [DisplayName("Order Number")]
        public int ID { get; set; }


        [Required (ErrorMessage ="Select Supplier")]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "Select Status")]
        public int StatusID { get; set; }

        [Required(ErrorMessage ="Select Date")]
        [DisplayName("Date of Order")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime InvoiceDate { get; set; }

       //[Required(ErrorMessage ="Status is required")]
       // public string Status { get; set; }


        public virtual Supplier Supplier { get; set; }
        public virtual Status Statuses { get; set; }
        public virtual IList<InvoiceItem> InvoiceItems { get; set; }

    }
}