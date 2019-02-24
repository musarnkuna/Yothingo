using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class UploadItem
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Upload a design")]
        public int DesignID { get; set; }

        [Required(ErrorMessage = "Enter Name")]
        public string Name { get; set; }

        public virtual Design Design { get; set; }
        public virtual Upload Upload { get; set; }
    }
}