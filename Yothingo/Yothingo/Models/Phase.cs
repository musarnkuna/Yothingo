using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Phase
    {
        public int ID { get; set; }

        [Required(ErrorMessage ="Phase Name Required ")]
        [DisplayName("Phase Name")]
        public string Name { get; set; }
    
        [Required(ErrorMessage = "Phase Description Required ")]
        //[StringLength(30, MinimumLength = 10, ErrorMessage = "Minimum 10 alphabetic characters")]
        public string Description { get; set; }

        public string PhaseDescription()
        {
            if (Name == "Phase 1")
            {
                Description = "Door cabin";
            }
            else if (Name == "Phase 2")
            {
                Description = "Panels";
            }
            else if (Name == "Phase 3")
            {
                Description = "Polystine";
            }
            else if (Name == "Phase 4")
            {
                Description = "Bongs";
            }
            return Description;
        }

    }
}