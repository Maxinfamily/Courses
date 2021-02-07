using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using WesserTask.Models;

namespace Wessertask.MyData
{
    public class ApplicantContext : DbContext
    {
        public ApplicantContext(DbContextOptions<ApplicantContext> options)
        : base(options)
        {
        }
        public DbSet<WesserTask.Models.Applicants> Applicants { get; set; }

        //public DbSet<Candidate> Candidates{ get; set; }
    }
}
