using ConsoleApp2;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ScoresController : Controller
    {
        private DatabaseService _dbService;

        public ScoresController()
        {
            _dbService = new DatabaseService();
        }

        public IActionResult Index()
        {
            List<StudentScores> scores = _dbService.GetStudentScores();
            return View(scores);
        }
    }
}
