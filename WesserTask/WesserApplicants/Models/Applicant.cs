using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WesserApplicants.Models
{
    public class Applicant
    {
        public int Id { get; set; }

        [Display(Name = "First name"), Required]
        public string FirstName { get; set; }

        [Display(Name = "Last Name"), Required]
        public string LastName { get; set; }

        [Display(Name = "Email address"), Required]
        [RegularExpression(@"^[a-zA-Z0-9]+[^ ]*\@[a-z]+\.[a-z]+$")]
        public string EmailAddress { get; set; }

        [RegularExpression(@"(\+[0-9]+ )?0?([0-9]{10}|[0-9]{4} [0-9]{6})")]
        [Display(Name = "Mobile number"), Required]
        public string MobileNumber { get; set; }

        [Display(Name = "House number")]
        public string HomeNumber { get; set; }

        [Display(Name = "Address line 1"), Required]
        public string Address1 { get; set; }

        [Display(Name = "Address line 2")]
        public string Address2 { get; set; }

        [Display(Name = "Address line 3")]
        public string Address3 { get; set; }

        [Required]
        public string Town { get; set; }
        public string County { get; set; }
        public string PostCode { get; set; }

        [Display(Name = "Starting date:")]
        [DataType(DataType.Date)]
        public DateTime StartingDate { get; set; }
    }
}
