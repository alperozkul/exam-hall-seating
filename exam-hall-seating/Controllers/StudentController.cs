using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IStudentRepository _studentRepository;

        public StudentController(ApplicationDbContext context, IStudentRepository studentRepository)
        {
            _context = context;
            _studentRepository = studentRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
            return View(students);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateStudentViewModel();
            viewModel.DepartmentList = _context.Departments
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList();
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel studentVM)
        {
            if (ModelState.IsValid)
            {
                var student = new Student
                {
                    Number = studentVM.Number,
                    FirstName = studentVM.FirstName,
                    LastName = studentVM.LastName,
                    Mail = studentVM.Mail,
                    Phone = studentVM.Phone,
                    Year = studentVM.Year,
                    Period = studentVM.Period,
                    DepartmentId = studentVM.DepartmentId,                                      
                };
                _studentRepository.Add(student);
                return RedirectToAction("Index");
            }
            
            return View(studentVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return View("Error");
            var studentVM = new EditStudentViewModel
            {
                Number = student.Number,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Mail = student.Mail,
                Phone = student.Phone,
                Year = student.Year,
                Period = student.Period,
                DepartmentId = student.DepartmentId,
                DepartmentList = _context.Departments
                .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name })
                .ToList()
            };
            return View(studentVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditStudentViewModel studentVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Edit işlemi gerçekleştirilemiyor.");
                return View("Edit", studentVM);
            }
            var student = new Student
            {
                Id = id,
                Number = studentVM.Number,
                FirstName = studentVM.FirstName,
                LastName = studentVM.LastName,
                Mail = studentVM.Mail,
                Phone = studentVM.Phone,
                Year = studentVM.Year,
                Period = studentVM.Period,
                DepartmentId = studentVM.DepartmentId,
            };
            _studentRepository.Update(student);
            return RedirectToAction("Index");
        }
    }
}
