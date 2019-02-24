using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Room
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Fridge Type is required")]
        [DisplayName("Fridge Type")]
        [StringLength(30)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Fridge Description is required")]
        [DisplayName("Fridge Description")]
        [StringLength(30)]
        public string FridgeDescription { get; set; }

        public virtual IList<Request> Requests { get; set; }
    }
}