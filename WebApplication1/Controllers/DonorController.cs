using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class DonorController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public DonorController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: /Donor
        public async Task<IActionResult> Index()
        {
            var donors = await _appDbContext.Donor
                .Include(d => d.BloodDonations)
                .OrderBy(d => d.DonorId)
                .ToListAsync();
            return View(donors);
        }

        // GET: /Donor/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _appDbContext.Donor
                .Include(d => d.BloodDonations)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // GET: /Donor/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Donor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Donor donor)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Donor.Add(donor);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Donor created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(donor);
        }

        // GET: /Donor/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _appDbContext.Donor.FindAsync(id);
            if (donor == null)
            {
                return NotFound();
            }
            return View(donor);
        }

        // POST: /Donor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Donor donor)
        {
            if (id != donor.DonorId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(donor);
                    await _appDbContext.SaveChangesAsync();
                    TempData["Message"] = "Donor updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DonorExists(donor.DonorId))
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
            return View(donor);
        }

        // GET: /Donor/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var donor = await _appDbContext.Donor
                .Include(d => d.BloodDonations)
                .FirstOrDefaultAsync(m => m.DonorId == id);
            if (donor == null)
            {
                return NotFound();
            }

            return View(donor);
        }

        // POST: /Donor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var donor = await _appDbContext.Donor.FindAsync(id);
            if (donor != null)
            {
                _appDbContext.Donor.Remove(donor);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Donor deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool DonorExists(int id)
        {
            return _appDbContext.Donor.Any(e => e.DonorId == id);
        }
    }
}
