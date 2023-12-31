﻿using AutoMapper;
using AutoMapper.Configuration.Conventions;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.StudentVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;


namespace exam_hall_seating.Controllers
{
    public class StudentController : Controller
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;

        public StudentController(IDepartmentRepository departmentRepository, IStudentRepository studentRepository, IMapper mapper, ApplicationDbContext context)
        {
            _departmentRepository = departmentRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Student> students = await _studentRepository.GetAllAsync();
            var pagedList = students.ToPagedList(page, 10);
            return View(pagedList);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStudentViewModel createStudentVM)
        {     
            if (ModelState.IsValid)
            {
                Student student = _mapper.Map<Student>(createStudentVM);  
                _studentRepository.Add(student);
                return RedirectToAction("Index");
            }     
            return View(createStudentVM);
        }
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");

            var student = await _studentRepository.GetByIdAsync(id);
            if (student == null) return View("Error");

            EditStudentViewModel editStudentVM = _mapper.Map<EditStudentViewModel>(student); 
            return View(editStudentVM);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditStudentViewModel editStudentVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Güncelleme işlemi gerçekleştirilemiyor.");
                return View("Edit", editStudentVM);
            }
            Student student = _mapper.Map<Student>(editStudentVM);
            _studentRepository.Update(student);
            return RedirectToAction("Index");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Student student = await _studentRepository.GetByIdAsync(id);
            _studentRepository.Delete(student);

            return RedirectToAction("Index");
        }
    }
}
