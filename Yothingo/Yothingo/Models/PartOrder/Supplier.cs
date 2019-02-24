using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace YothingoSprint1.Models
{
    public class Supplier
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [DisplayName("First Name")]
        [StringLength(30)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [DisplayName("Last Name")]
        [StringLength(30)]
        public string LastName { get; set; }

        [Required]
        [Display(Name = "Email")]
        [EmailAddress]
        [StringLength(30)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Company Name is required")]
        [DisplayName("Company Name")]
        [StringLength(30, MinimumLength = 5)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Example: 0712345678")]
        [StringLength(24)]
        public string Phone { get; set; }

        [Required]
        [StringLength(30)]
        public string Address { get; set; }

        public virtual IList<Invoice> Invoices { get; set; }
        public virtual IList<Quotation> Quotations { get; set; }
    }
}