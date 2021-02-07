using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WesserApplicants.Models
{
    public class ApplicantTownViewModel
    {
        public List<Applicant> Applicants {get; set;}
        public SelectList Towns { get; set;}
        public string ApplicantTown { get; set; }
        public string SearchString { get; set; }
    }
}
