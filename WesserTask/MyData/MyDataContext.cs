using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MyData
{
    public class MyDataContext : DbContext
    {
        public MyDataContext(DbContextOptions<MyDataContext> options)
        : base(options)
        {
        }

        //public DbSet<Candidate> Candidates{ get; set; }
    }
}
