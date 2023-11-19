using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.ExamVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Controllers
{
    public class ExamController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IExamRepository _examRepository;
        private readonly IMapper _mapper;

        public ExamController(ApplicationDbContext context, IExamRepository examRepository, IMapper mapper)
        {
            _context = context;
            _examRepository = examRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Exam> exams = await _examRepository.GetAllAsync();
            return View(exams);
        }

        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Lectures, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateExamViewModel createExamVM)
        {
            if (ModelState.IsValid)
            {
                Exam exam = _mapper.Map<Exam>(createExamVM);
                _examRepository.Add(exam);
                return RedirectToAction("Index");
            }
            return View(createExamVM);
        }

    }
}
