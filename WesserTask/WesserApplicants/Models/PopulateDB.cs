using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WesserApplicants.Data;

namespace WesserApplicants.Models
{
    public class PopulateDB
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicantContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicantContext>>()))
            {
                // Look for any movies.
                if (context.Applicants.Any())
                {
                    return;   // DB has been seeded
                }

                context.Applicants.AddRange(
                    new Applicant
                    {
                        FirstName = "First",
                        LastName = "First",
                        MobileNumber = "11111 111111",
                        Address1 = "1 Main road",
                        Town = "Town1",
                        County = "County1",
                        PostCode = "L11 1GH",
                        StartingDate = DateTime.Parse("2021-01-25 00:00:00.001")
                    },

                    new Applicant
                    {
                        FirstName = "Second",
                        LastName = "Second",
                        MobileNumber = "22222 222222",
                        Address1 = "2 Main road",
                        Town = "Town2",
                        County = "County2",
                        PostCode = "L22 2GH",
                        StartingDate = DateTime.Parse("2021-01-26 00:00:00.001")
                    },

                    new Applicant
                    {
                        FirstName = "Third",
                        LastName = "Third",
                        MobileNumber = "33333 3333333",
                        Address1 = "3 Main road",
                        Town = "Town3",
                        County = "County3",
                        PostCode = "L33 3GH",
                        StartingDate = DateTime.Parse("2021-01-27 00:00:00.001")
                    },

                    new Applicant
                    {
                        FirstName = "Fourth",
                        LastName = "Fourth",
                        MobileNumber = "44444 444444",
                        Address1 = "4 Main road",
                        Town = "Town4",
                        County = "County4",
                        PostCode = "L44 4GH",
                        StartingDate = DateTime.Parse("2021-01-28 00:00:00.001")
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
