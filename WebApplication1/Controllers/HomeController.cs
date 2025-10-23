using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        // Main dashboard view
        public async Task<IActionResult> Index()
        {
            try
            {
                var totalDonors = await _context.Donor.CountAsync();
                var totalRequests = await _context.BloodRequest.CountAsync();
                var totalDonations = await _context.BloodDonation.CountAsync();
                var totalInventory = await _context.BloodInventory.CountAsync();

                var recentRequests = await _context.BloodRequest
                    .Include(br => br.Recipient)
                    .OrderByDescending(br => br.RequestedDate)
                    .Take(5)
                    .ToListAsync();

                var recentDonations = await _context.BloodDonation
                    .Include(bd => bd.Donor)
                    .OrderByDescending(bd => bd.DonationDate)
                    .Take(5)
                    .ToListAsync();

                ViewBag.TotalDonors = totalDonors;
                ViewBag.TotalRequests = totalRequests;
                ViewBag.TotalDonations = totalDonations;
                ViewBag.TotalInventory = totalInventory;
                ViewBag.RecentRequests = recentRequests;
                ViewBag.RecentDonations = recentDonations;

                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading dashboard data");
                
                // Return default values if database is not ready
                ViewBag.TotalDonors = 0;
                ViewBag.TotalRequests = 0;
                ViewBag.TotalDonations = 0;
                ViewBag.TotalInventory = 0;
                ViewBag.RecentRequests = new List<BloodRequest>();
                ViewBag.RecentDonations = new List<BloodDonation>();

                TempData["Error"] = "Database not ready. Please run the database setup script.";
                return View();
            }
        }

        // Search donors by blood type and other criteria
        [HttpPost]
        public async Task<IActionResult> SearchDonor(string searchTerm)
        {
            try
            {
                if (string.IsNullOrEmpty(searchTerm))
                {
                    TempData["Error"] = "Please enter a search term.";
                    return RedirectToAction(nameof(Index));
                }

                var donors = await _context.Donor
                    .Where(d => d.FirstName.Contains(searchTerm) || 
                               d.LastName.Contains(searchTerm) || 
                               d.EmailAddress.Contains(searchTerm) ||
                               d.BloodType.Contains(searchTerm))
                    .ToListAsync();

                ViewBag.SearchResults = donors;
                ViewBag.SearchTerm = searchTerm;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error searching donors");
                TempData["Error"] = "Search failed. Please try again.";
            }

            return View("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
