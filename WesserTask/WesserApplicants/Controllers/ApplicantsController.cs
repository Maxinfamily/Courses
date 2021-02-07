using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WesserApplicants.Data;
using WesserApplicants.Models;

namespace WesserApplicants.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly ApplicantContext _context;

        public ApplicantsController(ApplicantContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string town, string searchString)
        {
            // Use LINQ to get list of Towns but the filter isnt working properly.
            IQueryable<string> townQuery = from a in _context.Applicants
                                            orderby a.Town
                                            select a.Town;

            var applicants = from a in _context.Applicants
                         select a;

            if (!string.IsNullOrEmpty(searchString))
            {
                applicants = applicants.Where(a => a.FirstName.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(town))
            {
                applicants = applicants.Where(a => a.Town == town);
            }

            var applicantTownVM = new ApplicantTownViewModel
            {
                Towns = new SelectList(await townQuery.Distinct().ToListAsync()),
                Applicants = await applicants.ToListAsync()
            };

            return View(applicantTownVM);
        }

        // GET: Applicants/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,EmailAddress,MobileNumber,HomeNumber,Address1,Address2,Address3,Town,County,PostCode,StartingDate")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,EmailAddress,MobileNumber,HomeNumber,Address1,Address2,Address3,Town,County,PostCode,StartingDate")] Applicant applicant)
        {
            if (id != applicant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicants.FindAsync(id);
            _context.Applicants.Remove(applicant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicants.Any(e => e.Id == id);
        }

        //To return the count
        public ActionResult CountApplicants()
        {
            var applicantsCount = _context.Applicants.Select(a => a.Id).Count();
            return View(applicantsCount);
        }
    }
}
