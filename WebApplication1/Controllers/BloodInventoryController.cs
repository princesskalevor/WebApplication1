using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BloodInventoryController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BloodInventoryController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: /BloodInventory
        public async Task<IActionResult> Index()
        {
            var bloodInventory = await _appDbContext.BloodInventory
                .OrderBy(bi => bi.UnitId)
                .ToListAsync();
            return View(bloodInventory);
        }

        // GET: /BloodInventory/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodInventory = await _appDbContext.BloodInventory
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (bloodInventory == null)
            {
                return NotFound();
            }

            return View(bloodInventory);
        }

        // GET: /BloodInventory/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /BloodInventory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] BloodInventory bloodInventory)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.BloodInventory.Add(bloodInventory);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Blood inventory unit created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(bloodInventory);
        }

        // GET: /BloodInventory/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodInventory = await _appDbContext.BloodInventory.FindAsync(id);
            if (bloodInventory == null)
            {
                return NotFound();
            }
            return View(bloodInventory);
        }

        // POST: /BloodInventory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] BloodInventory bloodInventory)
        {
            if (id != bloodInventory.UnitId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(bloodInventory);
                    await _appDbContext.SaveChangesAsync();
                    TempData["Message"] = "Blood inventory unit updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BloodInventoryExists(bloodInventory.UnitId))
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
            return View(bloodInventory);
        }

        // GET: /BloodInventory/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bloodInventory = await _appDbContext.BloodInventory
                .FirstOrDefaultAsync(m => m.UnitId == id);
            if (bloodInventory == null)
            {
                return NotFound();
            }

            return View(bloodInventory);
        }

        // POST: /BloodInventory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bloodInventory = await _appDbContext.BloodInventory.FindAsync(id);
            if (bloodInventory != null)
            {
                _appDbContext.BloodInventory.Remove(bloodInventory);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Blood inventory unit deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool BloodInventoryExists(int id)
        {
            return _appDbContext.BloodInventory.Any(e => e.UnitId == id);
        }
    }
}
