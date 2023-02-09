using BootCoin.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using BootCoin.Models.DBEntities;
using BootCoin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace BootCoin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _UserManager;

        public AccountController(ApplicationDbContext context, SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _SignInManager = signInManager;
            _UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            UserLogin _UserLogin = new UserLogin();
            return View(_UserLogin);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin userLogin, string returnUrl = null)
        {
            var result = await _SignInManager.PasswordSignInAsync(userLogin.Email, userLogin.Password, false, false);
            
            if (result.Succeeded)
            {
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return LocalRedirect(returnUrl);
            }
            return View(userLogin);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> TempLogin(Admin userInfo, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _SignInManager.PasswordSignInAsync(userInfo.Email, userInfo.Password, false, false);

                if (result.Succeeded)
                {
                    if (String.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }
                else
                {
                    TempData.Add("Message", "Invalid Email or Password");
                    return RedirectToAction("Login", "Account");
                }
            }

            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterModel registerModel = new RegisterModel();
            return View(registerModel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel registerModel)
        {
            var user = new ApplicationUser()
            {
                Email = registerModel.Email,
                UserName = registerModel.Email,
                AdminName = registerModel.UserName
            };
            var result = await _UserManager.CreateAsync(user, registerModel.Password);

            if (result.Succeeded)
            {
                // Add the user to the Admin table with AdminID "AA" + 2 digit number
                Admin admin = new Admin();
                admin.AdminID = "AA" + (_context.Admin.Count() + 1).ToString("D2");
                admin.Email = registerModel.Email;
                admin.Password = registerModel.Password;
                admin.AdminName = registerModel.UserName;
                _context.Admin.Add(admin);
                _context.SaveChanges();
                return RedirectToAction("Login", "Account");
            }

            return View(registerModel);
        }

    }
}

