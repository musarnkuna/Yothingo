using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Upload
    {

        //[DisplayName("Order Number")]
        public int ID { get; set; }

        [Required(ErrorMessage ="Select Date")]
        [DisplayName("Date of Order")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime UploadDate { get; set; }

        public virtual IList<UploadItem> UploadItems { get; set; }

    }
}