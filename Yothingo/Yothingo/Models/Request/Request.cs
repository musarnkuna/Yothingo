using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using YothingoSprint1.Models;

namespace YothingoSprint1.Models
{
    public class Request
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Request Number")]

        public string Request_Number { get; set; }

        public int RequestNo { get; set; }

        public int Number { get; set; }
      

        public string GetUniqueRequestNumber()
        {
            var ticks = DateTime.Now.Ticks;
            var ticksString = ticks.ToString();
            var ticksSubString = ticksString.Substring((ticksString.Length - 10 > 0) ? ticksString.Length - 10 : 0);
            if (this.RequestNo.Equals(ticks))
            {
                this.Number++;

                if (this.Number >= 999)
                {
                    System.Threading.Thread.Sleep(1);
                }

                return (Number + ticksSubString + this.Number.ToString("D4")).PadRight(20, '9');
            }

            this.Number = 2;
            this.RequestNo = 1;
            return (Number + ticksSubString).PadRight(11, '9');
        }

        [Required(ErrorMessage = "Select Date")]
        [DisplayName("Date of Order")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime RequestDate { get; set; }

        public string Width { get; set; }
        public string Length { get; set; }

        //public int RoomId { get; set; }

        //[ForeignKey("ID")]
        //public virtual Room Room { get; set; }

        [Required]
        [StringLength(130), MinLength(30), MaxLength(130)]
        public string Description { get; set; }

        public string RequestStatus { get; set; }

        public string ProblemDescription { get; set; }

        [ForeignKey("applicationUser")]
        public string UserId { get; set; }
        public virtual IList<Quotation> Quotations { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
    }
}