using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class QuotationItem
    {
        public int ID { get; set; }
        public int PartID { get; set; }
        public int QuotationID { get; set; }
        [Required]
        public int Quantity { get; set; }
        public virtual Part Part { get; set; }
        public virtual Quotation Quotation { get; set; }

        #region calculated fields
        public double Total
        {
            get
            {
                return Part == null ? 0 : Quantity * this.Part.UnitPrice;
            }
            protected set { } 
        }
        #endregion
    }
}