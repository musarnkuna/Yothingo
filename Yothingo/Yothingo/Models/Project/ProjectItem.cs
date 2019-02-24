using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class ProjectItem
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Choose a part")]
        public int PartID { get; set; }

        [Required]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "Enter Quantity")]
        public int Quantity { get; set; }

        public virtual Part Part { get; set; }
        public virtual Project Project { get; set; }

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