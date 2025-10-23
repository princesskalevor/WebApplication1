using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class BloodRequestController : Controller
    {
        // GET: /BloodRequest/Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: /BloodRequest/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(BloodRequest model)
        {
            if (ModelState.IsValid)
            {
                // TODO: save the request to database
                TempData["Message"] = "Your blood request has been submitted!";
                return RedirectToAction("Create");
            }
            return View(model);
        }
    }
}
