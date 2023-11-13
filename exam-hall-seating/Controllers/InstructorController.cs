using AutoMapper;
using exam_hall_seating.Data;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.InstructorVM;
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

        public InstructorController(UserManager<AppUser> userManager, ApplicationDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _context = context;
            _mapper = mapper;
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


        
    }
}
