using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Repository;
using exam_hall_seating.ViewModels.EnrollmentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace exam_hall_seating.Controllers
{
    public class EnrollmentController : Controller
    {
        private readonly ILectureRepository _lectureRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public EnrollmentController(ILectureRepository lectureRepository, IEnrollmentRepository enrollmentRepository, IMapper mapper)
        {
            _lectureRepository = lectureRepository;
            _enrollmentRepository = enrollmentRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {         
            ViewBag.Lectures = new SelectList(await _lectureRepository.GetAllAsync(), "Id", "Name");           
            return View(new AssignmentViewModel());
        }


        [HttpPost]
        public async Task<IActionResult> GetAllStudent(AssignmentViewModel assignmentVM)
        {
            ViewBag.Lectures = new SelectList(await _lectureRepository.GetAllAsync(), "Id", "Name");
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
