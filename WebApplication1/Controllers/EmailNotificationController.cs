using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class EmailNotificationController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public EmailNotificationController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        // GET: /EmailNotification
        public async Task<IActionResult> Index()
        {
            var emailNotifications = await _appDbContext.EmailNotification
                .Include(en => en.Recipient)
                .OrderByDescending(en => en.DateSent)
                .ToListAsync();
            return View(emailNotifications);
        }

        // GET: /EmailNotification/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailNotification = await _appDbContext.EmailNotification
                .Include(en => en.Recipient)
                .FirstOrDefaultAsync(m => m.NotificationId == id);
            if (emailNotification == null)
            {
                return NotFound();
            }

            return View(emailNotification);
        }

        // GET: /EmailNotification/Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Recipients = await _appDbContext.Recipient.ToListAsync();
            return View();
        }

        // POST: /EmailNotification/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromForm] EmailNotification emailNotification)
        {
            if (ModelState.IsValid)
            {
                emailNotification.DateSent = DateTime.Now;
                _appDbContext.EmailNotification.Add(emailNotification);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Email notification sent successfully!";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Recipients = await _appDbContext.Recipient.ToListAsync();
            return View(emailNotification);
        }

        // GET: /EmailNotification/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailNotification = await _appDbContext.EmailNotification.FindAsync(id);
            if (emailNotification == null)
            {
                return NotFound();
            }
            ViewBag.Recipients = await _appDbContext.Recipient.ToListAsync();
            return View(emailNotification);
        }

        // POST: /EmailNotification/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [FromForm] EmailNotification emailNotification)
        {
            if (id != emailNotification.NotificationId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _appDbContext.Update(emailNotification);
                    await _appDbContext.SaveChangesAsync();
                    TempData["Message"] = "Email notification updated successfully!";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmailNotificationExists(emailNotification.NotificationId))
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
            ViewBag.Recipients = await _appDbContext.Recipient.ToListAsync();
            return View(emailNotification);
        }

        // GET: /EmailNotification/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var emailNotification = await _appDbContext.EmailNotification
                .Include(en => en.Recipient)
                .FirstOrDefaultAsync(m => m.NotificationId == id);
            if (emailNotification == null)
            {
                return NotFound();
            }

            return View(emailNotification);
        }

        // POST: /EmailNotification/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var emailNotification = await _appDbContext.EmailNotification.FindAsync(id);
            if (emailNotification != null)
            {
                _appDbContext.EmailNotification.Remove(emailNotification);
                await _appDbContext.SaveChangesAsync();
                TempData["Message"] = "Email notification deleted successfully!";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EmailNotificationExists(int id)
        {
            return _appDbContext.EmailNotification.Any(e => e.NotificationId == id);
        }
    }
}
