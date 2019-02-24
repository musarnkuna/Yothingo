using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;

namespace YothingoSprint1.Models
{
    public partial class Event
    {      
        public int EventID { get; set; }

        [Required(ErrorMessage ="Please enter subject of the meeting")]
        [DisplayName("Subject")]
        public string Subject { get; set; }

        [Required(ErrorMessage ="Please enter description of the meeting that will take place")]
        [DisplayName("Description")]
        public string Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DisplayName("Date")]
        public System.DateTime Start { get; set; }
        public Nullable<System.DateTime> End { get; set; }
        public string ThemeColor { get; set; }
        public bool IsFullDay { get; set; }

    }
}