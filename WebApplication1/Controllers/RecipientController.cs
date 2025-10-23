using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class RecipientController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public RecipientController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: /Recipient
        public async Task<IActionResult> Index()
        {
            var recipients = await _appDbContext.Recipient
                .Include(r => r.BloodRequests)
                .Include(r => r.EmailNotifications)
                .OrderBy(r => r.RecipientId)
                .ToListAsync();
            return View(recipients);
        }

        // GET: /Recipient/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipient = await _appDbContext.Recipient
                .Include(r => r.BloodRequests)
                .Include(r => r.EmailNotifications)
                .FirstOrDefaultAsync(m => m.RecipientId == id);
            if (recipient == null)
            {
                return NotFound();
            }

            return View(recipient);
        }

        // GET: /Recipient/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Recipient/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] Recipient recipient)
        {
            if (ModelState.IsValid)
            {
                _appDbContext.Recipient.Add(recipient);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Recipient created successfully!";
                return RedirectToAction(nameof(Index));
            }
            return View(recipient);
        }

        // GET: /Recipient/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipient = await _appDbContext.Recipient.FindAsync(id);
            if (recipient == null)
            {
                return NotFound();
            }
            return View(recipient);
        }

        // POST: /Recipient/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] Recipient recipient)
        {
            if (id != recipient.RecipientId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(recipient);
                    await _appDbContext.SaveChangesAsync();
                    TempData["Message"] = "Recipient updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecipientExists(recipient.RecipientId))
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
            return View(recipient);
        }

        // GET: /Recipient/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recipient = await _appDbContext.Recipient
                .Include(r => r.BloodRequests)
                .Include(r => r.EmailNotifications)
                .FirstOrDefaultAsync(m => m.RecipientId == id);
            if (recipient == null)
            {
                return NotFound();
            }

            return View(recipient);
        }

        // POST: /Recipient/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var recipient = await _appDbContext.Recipient.FindAsync(id);
            if (recipient != null)
            {
                _appDbContext.Recipient.Remove(recipient);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Recipient deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool RecipientExists(int id)
        {
            return _appDbContext.Recipient.Any(e => e.RecipientId == id);
        }
    }
}
