using AutoMapper;
using AutoMapper.Features;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.Repository;
using exam_hall_seating.Services.Interfaces;
using exam_hall_seating.ViewModels.ExamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace exam_hall_seating.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly ILectureRepository _lectureRepository; //
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IClassroomRepository _classroomRepository;
        private readonly IClassroomDetailRepository _classroomDetailRepository;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;

        public ExamController(IExamRepository examRepository, ILectureRepository lectureRepository, IMapper mapper, IEnrollmentRepository enrollmentRepository, IExcelService excelService, IClassroomRepository classroomRepository, IClassroomDetailRepository classroomDetailRepository)
        {
            _examRepository = examRepository;
            _lectureRepository = lectureRepository;//
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _excelService = excelService;
            _classroomRepository = classroomRepository;
            _classroomDetailRepository = classroomDetailRepository;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Exam> exams = await _examRepository.GetAllAsync();
            var pagedList = exams.ToPagedList(page, 10);
            return View(pagedList);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Lectures = new SelectList(await _lectureRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExamViewModel createExamVM)
        {
            ViewBag.Lectures = new SelectList(await _lectureRepository.GetAllAsync(), "Id", "Name");

            if (ModelState.IsValid)
            {
                Exam exam = _mapper.Map<Exam>(createExamVM);
                _examRepository.Add(exam);
                return RedirectToAction("Index");
            }
            return View(createExamVM);
        }
        
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Lectures = new SelectList(await _lectureRepository.GetAllAsync(), "Id", "Name");
            var exam = await _examRepository.GetByIdAsync(id);
            if (exam == null) return View("Error");

            EditExamViewModel editExamVM = _mapper.Map<EditExamViewModel>(exam);
            return View(editExamVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditExamViewModel editExamVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Güncelleme işlemi gerçekleştirilemiyor.");
                return View("Error", editExamVM);
            }
            Exam exam = _mapper.Map<Exam>(editExamVM);
            _examRepository.Update(exam);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Exam exam = await _examRepository.GetByIdAsync(id);
            _examRepository.Delete(exam);
            return RedirectToAction("Index");
        }

        
        public async Task<IActionResult> Arrangement(int id)
        {
            var exam = await _examRepository.GetByIdAsync(id);
            var enrolledStudents = await _enrollmentRepository.GetAllStudentsByLectureId(exam.LectureId);

            if (exam == null) return View("Error");

            ArrangementViewModel arrangementVM = new ArrangementViewModel();

            arrangementVM.Id = id;
            arrangementVM.Date = exam.Date;
            arrangementVM.StartTime = exam.StartTime;
            arrangementVM.EndTime = exam.EndTime;
            arrangementVM.LectureName = exam.Lecture.Name;
            arrangementVM.LectureId = exam.Lecture.Id;

            arrangementVM.Students = enrolledStudents;


            ViewBag.Classrooms = new SelectList(await _classroomRepository.GetAllAsync(), "Id", "ClassName");

            return View(arrangementVM);
        }

        [HttpPost]
        public async Task<IActionResult> ArrangementWithExcel(ArrangementViewModel arrangementVM, IFormFile file)
        {
            ViewBag.Classrooms = new SelectList(await _classroomRepository.GetAllAsync(), "Id", "ClassName");

            var exam = await _examRepository.GetByIdAsync(arrangementVM.Id);
            var lecture = await _lectureRepository.GetByIdAsync(exam.LectureId);
            arrangementVM.Date = exam.Date;
            arrangementVM.StartTime = exam.StartTime;
            arrangementVM.EndTime = exam.EndTime;
            arrangementVM.LectureName = exam.Lecture.Name;
            arrangementVM.LectureId = exam.Lecture.Id;

            List<EnrolledStudentViewModel> enrolledStudents = _excelService.ReadExcelFileAsync(file);
            arrangementVM.Students = enrolledStudents;
            return View("Arrangement", arrangementVM);
            
        }


        [HttpPost]
        public async Task<IActionResult> ClassArrangement(ArrangementViewModel arrangementVM)
        {
            ViewBag.Classrooms = new SelectList(await _classroomRepository.GetAllAsync(), "Id", "ClassName");
            var exam = await _examRepository.GetByIdAsync(arrangementVM.Id);
            var lecture = await _lectureRepository.GetByIdAsync(exam.LectureId);
            arrangementVM.Date = exam.Date;
            arrangementVM.StartTime = exam.StartTime;
            arrangementVM.EndTime = exam.EndTime;
            arrangementVM.LectureName = exam.Lecture.Name;
            arrangementVM.LectureId = exam.Lecture.Id;


            int totalStudent = arrangementVM.Students.Count;
            List<Classroom> classroomList = new List<Classroom>();

            foreach(var id in arrangementVM.SelectedClassrooms)
            {
                Classroom classroom = await _classroomRepository.GetByIdAsync(id);
                classroomList.Add(classroom);
            }

            int currentStudent = 0;
            foreach (var classroom in classroomList)
            {
                int totalCapacity = (int)classroom.ExamCapacity;
                for(int i = 0; i < totalCapacity && currentStudent < totalStudent; i++)
                {
                    arrangementVM.Students[currentStudent].ClassName = classroom.ClassName;
                    currentStudent++;
                }
            }

            return View("Arrangement", arrangementVM);
        }

    }
}
