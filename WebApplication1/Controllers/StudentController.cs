//using ConsoleApp2;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class StudentController : Controller
    {
        //private DatabaseService _dbService;
        private AppDbContext _appDbContext;

        public StudentController(AppDbContext appDbContext)
        {
            //_dbService = new DatabaseService();
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            //List<Student> students = _dbService.GetStudents();

            List<Student> students = _appDbContext.Student.OrderBy(s => s.StudentId).ToList();
            return View(students);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost] 
        public IActionResult Add([FromForm] Student request)
        {
            //_dbService.AddStudent(request.FirstName, request.LastName, 
            //    request.Gender, request.BirthDate);

            _appDbContext.Student.Add(request);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Edit(int id)
        {
            //Student student = _dbService.GetStudent(id);

            Student student = _appDbContext.Student.Find(id);
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit([FromForm] Student request)
        {
            //_dbService.updateStudent(request);
            _appDbContext.Student.Update(request);
            _appDbContext.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        //public IActionResult Delete(int id)
        //{
        //    //Student student = _dbService.GetStudent(id);

        //    return View(student);
        //}

        [HttpPost]
        public IActionResult Delete([FromForm] Student request)
        {
            //_dbService.deleteStudent(request.StudentId);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Filter()
        {
            return View();
        }
        //[HttpPost]
        //public IActionResult Filter([FromForm] YearFilter filter)
        //{
            
        //    List<Student> students = _dbService.FilterStudents(filter.StartingYear);
        //    return View(nameof(Index), students);
        //}
    }
}
