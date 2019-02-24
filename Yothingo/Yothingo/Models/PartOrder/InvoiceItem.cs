using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class InvoiceItem
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Choose a part")]
        public int PartID { get; set; }

        [Required]
        public int InvoiceID { get; set; }

        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }

        public virtual Part Part { get; set; }
        public virtual Invoice Invoice { get; set; }

        #region calculated fields
        //public double Total
        //{
        //    get
        //    {
        //        return Part == null ? 0 : Quantity /** this.Part.UnitPrice*/;
        //    }
        //    protected set { } 
        //}
        #endregion
    }
}