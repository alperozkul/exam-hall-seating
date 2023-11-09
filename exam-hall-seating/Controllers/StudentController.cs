using exam_hall_seating.Data;
using exam_hall_seating.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Student> students = _context.Students.Include(a => a.Department).ToList();
            return View(students);
        }
    }
}
