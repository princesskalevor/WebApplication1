using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BloodDonationController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BloodDonationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: /BloodDonation
        public async Task<IActionResult> Index()
        {
            var bloodDonations = await _appDbContext.BloodDonation
                .Include(bd => bd.Donor)
                .OrderByDescending(bd => bd.DonationDate)
                .ToListAsync();
            return View(bloodDonations);
        }

        // GET: /BloodDonation/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonation = await _appDbContext.BloodDonation
                .Include(bd => bd.Donor)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            return View(bloodDonation);
        }

        // GET: /BloodDonation/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Donors = await _appDbContext.Donor.ToListAsync();
            return View();
        }

        // POST: /BloodDonation/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BloodDonation bloodDonation)
        {
            if (ModelState.IsValid)
            {
                bloodDonation.DonationDate = DateOnly.FromDateTime(DateTime.Now);
                _appDbContext.BloodDonation.Add(bloodDonation);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Blood donation recorded successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Donors = await _appDbContext.Donor.ToListAsync();
            return View(bloodDonation);
        }

        // GET: /BloodDonation/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonation = await _appDbContext.BloodDonation.FindAsync(id);
            if (bloodDonation == null)
            {
                return NotFound();
            }
            ViewBag.Donors = await _appDbContext.Donor.ToListAsync();
            return View(bloodDonation);
        }

        // POST: /BloodDonation/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BloodDonation bloodDonation)
        {
            if (id != bloodDonation.DonationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(bloodDonation);
                    await _appDbContext.SaveChangesAsync();
                    TempData["Message"] = "Blood donation updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodDonationExists(bloodDonation.DonationId))
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
            ViewBag.Donors = await _appDbContext.Donor.ToListAsync();
            return View(bloodDonation);
        }

        // GET: /BloodDonation/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodDonation = await _appDbContext.BloodDonation
                .Include(bd => bd.Donor)
                .FirstOrDefaultAsync(m => m.DonationId == id);
            if (bloodDonation == null)
            {
                return NotFound();
            }

            return View(bloodDonation);
        }

        // POST: /BloodDonation/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloodDonation = await _appDbContext.BloodDonation.FindAsync(id);
            if (bloodDonation != null)
            {
                _appDbContext.BloodDonation.Remove(bloodDonation);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Blood donation deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BloodDonationExists(int id)
        {
            return _appDbContext.BloodDonation.Any(e => e.DonationId == id);
        }
    }
}
