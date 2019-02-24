using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Design
    {
        internal string FileName;

        public class MyViewModel
        {
            [DisplayName("Select File to Upload")]
            public IEnumerable<HttpPostedFileBase> Image { get; set; }
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DesignId { get; set; }

        [Required]
        [DisplayName("Design Name")]
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public string ImageType { get; set; }

        //[Required(ErrorMessage = "Select Date")]
        //[DisplayName("Upload Date")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //public DateTime UploadDate { get; set; }

        public int ID { get; set; }
        [ForeignKey("ID")]
        public Request Request { get; set; }

        [ForeignKey("applicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }


    }
}