using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.Repository;
using exam_hall_seating.ViewModels.LectureVM;
using exam_hall_seating.ViewModels.StudentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace exam_hall_seating.Controllers
{
    public class LectureController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILectureRepository _lectureRepository;
        private readonly IMapper _mapper;

        public LectureController(IDepartmentRepository departmentRepository, ILectureRepository lectureRepository, IMapper mapper)
        {
            _departmentRepository = departmentRepository;
            _lectureRepository = lectureRepository;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Lecture> lectures = await _lectureRepository.GetAllAsync();
            var pagedList = lectures.ToPagedList(page, 10);
            return View(pagedList);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateLectureViewModel createLectureVM)
        {
            if (ModelState.IsValid)
            {
                Lecture lecture = _mapper.Map<Lecture>(createLectureVM);
                _lectureRepository.Add(lecture);
                return RedirectToAction("Index");
            }
            return View(createLectureVM);
        }


        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");

            var lecture = await _lectureRepository.GetByIdAsync(id);
            if (lecture == null) return View("Error");

            EditLectureViewModel editLectureVM = _mapper.Map<EditLectureViewModel>(lecture);
            return View(editLectureVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditLectureViewModel editLectureVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Güncelleme işlemi gerçekleştirilemiyor.");
                return View("Edit", editLectureVM);
            }
            Lecture lecture = _mapper.Map<Lecture>(editLectureVM);
            _lectureRepository.Update(lecture);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Lecture lecture = await _lectureRepository.GetByIdAsync(id);
            _lectureRepository.Delete(lecture);

            return RedirectToAction("Index");
        }
    }
}
