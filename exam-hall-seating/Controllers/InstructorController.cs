using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.InstructorVM;
using exam_hall_seating.ViewModels.StudentVM;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace exam_hall_seating.Controllers
{
    public class InstructorController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly SignInManager<AppUser> _signInManager;

        public InstructorController(UserManager<AppUser> userManager, ApplicationDbContext context, IMapper mapper, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
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


        public IActionResult Create()
        {
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");
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
            ViewBag.Departments = new SelectList(_context.Departments, "Id", "Name");

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

    }
}
