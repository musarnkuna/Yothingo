using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Quotation
    {
        [Key]
        [DisplayName("Quotation Number")]
        public int QuotationID { get; set; }

        [Required(ErrorMessage = "Select Status")]
        public int StatusID { get; set; }

        [Required(ErrorMessage = "Select Date")]
        [DisplayName("Date of Order")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime QuotationDate { get; set; }

        [DisplayName("Tax Percentage")]
        public double SalesTaxPercent { get; set; }

        public int RequestID { get; set; }


        //public string Status { get; set; }
        public virtual Request Request { get; set; }
        public virtual Status Statuses { get; set; }
        public virtual IList<QuotationItem> QuotationItems { get; set; }


        //[ForeignKey("applicationUser")]
        //public string UserId { get; set; }
        //public virtual ApplicationUser applicationUser { get; set; }


        #region Calculated Fields
        [DisplayName("Total Sales Tax")]
        public double TotalSalesTax
        {
            get
            {
                SalesTaxPercent = 15;
                return SubTotal * SalesTaxPercent / 100;
            }

            set { }
        }

        [DisplayName("Sub Total")]
        public double SubTotal
        {
            get
            {
                return QuotationItems == null ? 0 : QuotationItems.Sum(item => item.Total);
            }
            set { }
        }

        [DisplayName("Grand Total")]
        public double GrandTotal
        {
            get
            {
                return SubTotal + TotalSalesTax;
            }
            set { }
        }

        #endregion
    }
}