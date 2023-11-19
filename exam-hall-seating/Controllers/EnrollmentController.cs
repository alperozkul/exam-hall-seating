using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.ViewModels.EnrollmentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace exam_hall_seating.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public EnrollmentController(ApplicationDbContext context, IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _context = context;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public IActionResult Index()
        {         
            ViewBag.Lectures = new SelectList(_context.Lectures, "Id", "Name");           
            return View(new AssignmentViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> GetAllStudent(AssignmentViewModel assignmentVM)
        {
            ViewBag.Lectures = new SelectList(_context.Lectures, "Id", "Name");
            if (ModelState.IsValid)
            {
                assignmentVM.Students = await _enrollmentRepository.GetAllStudentByLectureAsync(assignmentVM.LectureId);
            }
            else
            {
            }
            return View("Index", assignmentVM);
        }

        [HttpPost]
        public async Task<IActionResult> EnrollStudents(AssignmentViewModel assignmentVM)
        {
            if (ModelState.IsValid)
            {
                await _enrollmentRepository.EnrollStudentsAsync(assignmentVM.LectureId, assignmentVM.Students);
                return RedirectToAction("Index");
            }
            else
            {
                // Validation errors occurred, handle accordingly
            }

            return View("Index", assignmentVM);
        }
    }
}
