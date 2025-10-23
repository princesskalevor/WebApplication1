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
        public IActionResult Index()
        {
            var model = new DashboardViewModel
            {
                TotalDonors = _context.Student.Count(),

                TotalRequests = _context.BloodRequests.Count(),

                // Fully qualify BloodRequest in LINQ to avoid ambiguity
                TotalDonations = _context.BloodRequests
                    .Count((WebApplication1.Models.BloodRequest r) => r.Status == "Verified"),

                RecentRequests = _context.BloodRequests
                    .OrderByDescending((WebApplication1.Models.BloodRequest r) => r.RequestId)
                    .Take(3)
                    .ToList(),

                AvailableUnits = 100 // Example static value
            };

            return View(model);
        }

        // Search donors by blood type and hall
        [HttpPost]
        public IActionResult SearchDonor(string bloodType, string hall)
        {
            var results = _context.Student
                .Where(s => (string.IsNullOrEmpty(bloodType) || s.BloodType == bloodType)
                         && (string.IsNullOrEmpty(hall) || s.Hall == hall))
                .Select(s => new
                {
                    Name = $"{s.FirstName} {s.LastName}",
                    BloodType = s.BloodType,
                    Hall = s.Hall,
                    Department = s.Department
                })
                .ToList();

            return PartialView("_DonorResultsPartial", results);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
