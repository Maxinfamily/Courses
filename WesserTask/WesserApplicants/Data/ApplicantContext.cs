using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WesserApplicants.Models;

namespace WesserApplicants.Data
{
    public class ApplicantContext : DbContext
    {
        public ApplicantContext(DbContextOptions<ApplicantContext> options)
        : base(options)
        {
        }
        public DbSet<WesserApplicants.Models.Applicant> Applicants { get; set; }

        //public DbSet<Candidate> Candidates{ get; set; }
    }
}
 