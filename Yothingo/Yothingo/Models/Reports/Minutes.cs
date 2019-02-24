using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YothingoSprint1.Models.Reports
{
    public class Minutes
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int MeetingMinutesId { get; set; }
        public string Id { get; set; }
        public ApplicationUser applicationUser { get; set; }
        public int EventID { get; set; }

        public Event Event { get; set; }

        [DisplayName("Attendees")]
        [MaxLength(500)]
        public string Attendees { get; set; }

        [DisplayName("Details of Meeting")]
        [Required(ErrorMessage = "Details of the Meeting")]
        [MaxLength(500)]
        public string Details { get; set; }

        [DisplayName("Date of Meeting")]
        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

    }
}