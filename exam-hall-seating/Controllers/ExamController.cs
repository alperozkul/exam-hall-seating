﻿using AutoMapper;
using AutoMapper.Features;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.Repository;
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
        private readonly ILectureRepository _lectureRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IMapper _mapper;

        public ExamController(IExamRepository examRepository, ILectureRepository lectureRepository, IMapper mapper, IEnrollmentRepository enrollmentRepository)
        {
            _examRepository = examRepository;
            _lectureRepository = lectureRepository;
            _mapper = mapper;
            _enrollmentRepository = enrollmentRepository;
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
            var lecture = await _lectureRepository.GetByIdAsync(exam.LectureId);
            var enrolledStudents = await _enrollmentRepository.GetAllStudentsByLectureId(exam.LectureId);

            if (exam == null) return View("Error");

            ArrangementViewModel arrangementVM = new ArrangementViewModel();

            arrangementVM.Id = id;
            arrangementVM.Date = exam.Date;
            arrangementVM.StartTime = exam.StartTime;
            arrangementVM.EndTime = exam.EndTime;
            arrangementVM.LectureName = lecture.Name;
            arrangementVM.LectureId = lecture.Id;
            arrangementVM.Students = enrolledStudents;

            return View(arrangementVM);
        }

    }
}
