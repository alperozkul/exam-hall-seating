using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Interfaces;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.InstructorVM;
using exam_hall_seating.ViewModels.StudentVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Runtime.InteropServices;

namespace exam_hall_seating.Controllers
{
    public class InstructorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ILectureRepository _lectureRepository;
        private readonly IInstructorLectureRepository _instructorLectureRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly SignInManager<AppUser> _signInManager;

        public InstructorController(UserManager<AppUser> userManager, IDepartmentRepository departmentRepository, ILectureRepository lectureRepository, IInstructorLectureRepository instructorLectureRepository, IMapper mapper, IHttpContextAccessor httpContextAccessor, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _departmentRepository = departmentRepository;
            _lectureRepository = lectureRepository;
            _instructorLectureRepository = instructorLectureRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            var instructors = _userManager.Users
                .Where(user => user.DepartmentId != null)
                .Include(i => i.Department)
                .ToList();
            return View(instructors);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateInstructorViewModel createInstructorVM)
        {
            if (!ModelState.IsValid) return View(createInstructorVM);
            
            var user = await _userManager.FindByEmailAsync(createInstructorVM.Email);
            if (user != null) 
            {
                TempData["Error"] = "Bu email adresi kullanılmaktadır.";
                return View(createInstructorVM);
            }

            AppUser appUser = _mapper.Map<AppUser>(createInstructorVM);
            var result = await _userManager.CreateAsync(appUser, createInstructorVM.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, AppRole.Instructor);
                return RedirectToAction("Index", "Instructor");
            }
            return View(createInstructorVM);   
        }

        public async Task<IActionResult> Edit(string id)
        {
            ViewBag.Departments = new SelectList(await _departmentRepository.GetAllAsync(), "Id", "Name");

            var instructor = await _userManager.FindByIdAsync(id);
            if (instructor == null) return View("Error");

            EditInstructorViewModel editInstructorVM = _mapper.Map<EditInstructorViewModel>(instructor);
            return View(editInstructorVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditInstructorViewModel editInstructorVM)
        {
            if (ModelState.IsValid)
            {
                var instructor = await _userManager.FindByIdAsync(id);
                instructor.FirstName = editInstructorVM.FirstName;
                instructor.LastName = editInstructorVM.LastName;
                instructor.Email = editInstructorVM.Email;
                instructor.PhoneNumber = editInstructorVM.PhoneNumber;
                instructor.DepartmentId = editInstructorVM.DepartmentId;

                IdentityResult result = await _userManager.UpdateAsync(instructor);
                if (!result.Succeeded)
                {
                    result.Errors.ToList().ForEach(e => ModelState.AddModelError(e.Code, e.Description));
                    return View(editInstructorVM);
                }
                await _userManager.UpdateSecurityStampAsync(instructor);
                await _signInManager.SignOutAsync();
                await _signInManager.SignInAsync(instructor, true);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            AppUser appUser = await _userManager.FindByIdAsync(id);
            await _userManager.DeleteAsync(appUser);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> AttendLecture()
        {

            await InstructorLectureViewBag();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AttendLecture(AttendInstructorViewModel attendInstructorVM)
        {
            await InstructorLectureViewBag();

            if (ModelState.IsValid)
            {
                InstructorLecture instructorLecture = _mapper.Map<InstructorLecture>(attendInstructorVM);
                _instructorLectureRepository.Add(instructorLecture);
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> ListLectures(string id)
        {
            var curUser = string.IsNullOrEmpty(id) ? _httpContextAccessor.HttpContext.User.GetUserId() : id;
            var instructorLectures = await _instructorLectureRepository.ShowInstructorLectures(curUser);

            var listLectureVM = instructorLectures.Select(il => new ListLecturesViewModel
            {
                InstructorLectureId = il.Id,
                AppUserId = id,
                FirstName = il.AppUser.FirstName,
                LastName = il.AppUser.LastName,
                Email = il.AppUser.Email,
                AppUser = il.AppUser,
                Lecture = il.Lecture,
                LectureId = il.LectureId,
                Code = il.Lecture.Code,
                Name = il.Lecture.Name,
                Year = il.Lecture.Year,
                Period = il.Lecture.Period
            }).ToList();
            return View(listLectureVM);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteLecture(int id)
        {
            InstructorLecture instructorLecture = await _instructorLectureRepository.GetByIdAsync(id);
            _instructorLectureRepository.Delete(instructorLecture);
            return RedirectToAction("Index");
        }
        private async Task InstructorLectureViewBag()
        {
            ViewBag.Lectures = new SelectList(
                (await _lectureRepository.GetAllAsync())
                .OrderBy(lecture => lecture.Name)
                , "Id", "Name");
            ViewBag.Instructors = new SelectList(await _userManager.Users
                .Where(user => user.DepartmentId != null)
                .OrderBy(instructor => instructor.FirstName)
                .ToListAsync(),
                "Id", "FullName");
        }
    }
}
