using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YothingoSprint1.Models
{
    public class RequestMeeting
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MeetingRequestId { get; set; }

        [DisplayName("Agenda")]
        [MaxLength(500)]
        public string Agenda { get; set; }

        [DisplayName("Brief Descritption of Meeting")]
        [Required(ErrorMessage = "Brief description of the Meeting is required")]
        [MaxLength(500)]
        public string Description { get; set; }

        [DisplayName("Date of Meeting")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required(ErrorMessage = "You must provide a phone number")]
        [Display(Name = "Telephone Number")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string Telephone { get; set; }


        public string RequestStatus { get; set; }

        [ForeignKey("applicationUser")]
        public string UserId { get; set; }
        public virtual ApplicationUser applicationUser { get; set; }
    }
}
