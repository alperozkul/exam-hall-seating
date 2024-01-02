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
using System.Reflection.Metadata;
using X.PagedList;

namespace exam_hall_seating.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository _examRepository;
        private readonly ILectureRepository _lectureRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IClassroomRepository _classroomRepository;

        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;
        private readonly IPdfService _pdfService;
        private readonly IMailService _mailService;
        private readonly IAlgorithmService _algorithmService;

        private static List<SelectListItem> _classroomSelectList;

        public ExamController(IExamRepository examRepository, ILectureRepository lectureRepository, IMapper mapper, IEnrollmentRepository enrollmentRepository, IExcelService excelService, IClassroomRepository classroomRepository, IPdfService pdfService, IMailService mailService, IAlgorithmService algorithmService)
        {
            _examRepository = examRepository;
            _lectureRepository = lectureRepository;//
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
            _excelService = excelService;
            _classroomRepository = classroomRepository;
            _pdfService = pdfService;
            _mailService = mailService;
            _algorithmService = algorithmService;

            if (_classroomSelectList == null)
            {
                InitializeClassroomSelectList();
            }
            
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
            ArrangementViewModel arrangementVM = new ArrangementViewModel
            {
                Id = id,
                Date = exam.Date,
                StartTime = exam.StartTime,
                EndTime = exam.EndTime,
                LectureName = exam.Lecture.Name,
                LectureId = exam.Lecture.Id,
                Students = enrolledStudents,
                SelectedAlgorithm = "Rastgele"
            };
            ViewBag.Classrooms = new SelectList(_classroomSelectList, "Value", "Text");
            ViewBag.AlgorithmOptions = new SelectList(new List<string>
            {
                "Rastgele",
                "Başarı Sıralamasına Göre",
                "Giriş Yılına Göre",
                "Giriş Yılı ve Başarı Sıralamasına Göre"
            });
            return View(arrangementVM);
        }

        [HttpPost]
        public async Task<IActionResult> ArrangementWithExcel(ArrangementViewModel arrangementVM, IFormFile file)
        {
            ViewBag.Classrooms = new SelectList(_classroomSelectList, "Value", "Text");
            ViewBag.AlgorithmOptions = new SelectList(new List<string>
            {
                "Rastgele",
                "Başarı Sıralamasına Göre",
                "Giriş Yılına Göre",
                "Giriş Yılı ve Başarı Sıralamasına Göre"
            });
            var exam = await _examRepository.GetByIdAsync(arrangementVM.Id);
            arrangementVM.Date = exam.Date;
            arrangementVM.StartTime = exam.StartTime;
            arrangementVM.EndTime = exam.EndTime;
            arrangementVM.LectureName = exam.Lecture.Name;
            arrangementVM.LectureId = exam.Lecture.Id;

            List<EnrolledStudentViewModel> enrolledStudents = await _excelService.ReadExcelFileAsync(file);
            
            //Random random = new Random();
            //enrolledStudents = enrolledStudents.OrderBy(student => random.Next()).ToList();

            //List<EnrolledStudentViewModel> sortedStudents = enrolledStudents
            //.OrderBy(s => s.Number.ToString().Substring(4, 4))   // Başarı sırasına göre sırala
            //.ThenBy(s => s.Number.ToString().Substring(0, 2)) // Giriş yılına göre sırala
            //.ToList();


            arrangementVM.Students = enrolledStudents;
            return View("Arrangement", arrangementVM);
            
        }


        [HttpPost]
        public async Task<IActionResult> ClassArrangement(ArrangementViewModel arrangementVM)
        {
            ViewBag.Classrooms = new SelectList(_classroomSelectList, "Value", "Text");
            ViewBag.AlgorithmOptions = new SelectList(new List<string>
            {
                "Rastgele",
                "Başarı Sıralamasına Göre",
                "Giriş Yılına Göre",
                "Giriş Yılı ve Başarı Sıralamasına Göre"
            });
            var exam = await _examRepository.GetByIdAsync(arrangementVM.Id);
            var lecture = await _lectureRepository.GetByIdAsync(exam.LectureId);
            arrangementVM.Date = exam.Date;
            arrangementVM.StartTime = exam.StartTime;
            arrangementVM.EndTime = exam.EndTime;
            arrangementVM.LectureName = exam.Lecture.Name;
            arrangementVM.LectureId = exam.Lecture.Id;


            int totalStudent = arrangementVM.Students.Count;
            List<Classroom> classroomList = new List<Classroom>();
            if(arrangementVM.SelectedClassrooms != null)
            {
                foreach (var id in arrangementVM.SelectedClassrooms)
                {
                    Classroom classroom = await _classroomRepository.GetByIdAsync(id);
                    classroomList.Add(classroom);
                }

                classroomList = classroomList.OrderByDescending(c => c.ExamCapacity).ToList();
                arrangementVM.Students = _algorithmService.SortRandom(arrangementVM.Students);

                List<EnrolledStudentViewModel> students = new List<EnrolledStudentViewModel>();

                int currentStudent = 0;
                foreach (var classroom in classroomList)
                {
                    int totalCapacity = (int)classroom.ExamCapacity;
                    for (int i = 0; i < totalCapacity && currentStudent < totalStudent; i++)
                    {
                        arrangementVM.Students[currentStudent].ClassName = classroom.ClassName;
                        currentStudent++;
                    }
                    List<EnrolledStudentViewModel> studentsWithCurrentClass = arrangementVM.Students.Where(x => x.ClassName == classroom.ClassName).ToList();
                    studentsWithCurrentClass = _algorithmService.SortByAlgorithm(studentsWithCurrentClass, arrangementVM.SelectedAlgorithm);
                    students.AddRange(studentsWithCurrentClass);
                }
                students.AddRange(arrangementVM.Students.Where(x => x.ClassName == null));
                arrangementVM.Students = students;
            } 
            return View("Arrangement", arrangementVM);
        }

        [HttpPost]
        public IActionResult DownloadPdf(ArrangementViewModel arrangementVM)
        {
            byte[] pdf = _pdfService.GeneratePdfAsync(arrangementVM);
            return File(pdf, "application/pdf", 
                arrangementVM.Date.ToString("dd/MM/yyyy")+"_"+
                arrangementVM.LectureName+"_"+
                arrangementVM.Students[0].ClassName+".pdf");
        }

        [HttpPost]
        public IActionResult SendMail(ArrangementViewModel arrangementVM)
        {
            try
            {
                byte[] pdf = _pdfService.GeneratePdfAsync(arrangementVM);
                _mailService.SendMail(arrangementVM, pdf);
                return Json(new { success = true, message = "Mail başarıyla gönderildi." });

            }
            catch
            {
                return Json(new { success = false, message = "Mail gönderme işleminde hata oluştu." });
            }

        }

        private void InitializeClassroomSelectList()
        {
            List<Classroom> classrooms = _classroomRepository.GetAll();
            _classroomSelectList = classrooms.Select(classroom => new SelectListItem
            {
                Value = classroom.Id.ToString(),
                Text = $"{classroom.ClassName} / Kapasite: {classroom.ExamCapacity}"
            }).ToList();           
        }

    }
}
