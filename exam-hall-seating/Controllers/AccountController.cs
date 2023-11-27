using exam_hall_seating.Data;
using exam_hall_seating.Models;
using exam_hall_seating.ViewModels.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace exam_hall_seating.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            
        }
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);
            
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);

            if(user == null)
            {
                TempData["Error"] = "Bilgiler yanlış. Lütfen tekrar deneyiniz.";
                return View(loginViewModel);
            }

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
            if (passwordCheck)
            {
                var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }               
            }
            TempData["Error"] = "Bilgiler yanlış. Lütfen tekrar deneyiniz."; //TempData kullanmak pek sağlıklı değil.
            return View(loginViewModel);
            

        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
