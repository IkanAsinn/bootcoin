using BootCoin.Data;
using BootCoin.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace BootCoin.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<IdentityUser> _SignInManager;
        private readonly UserManager<IdentityUser> _UserManager;

        public AccountController(ApplicationDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._SignInManager = signInManager;
            this._UserManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            UserLogin _UserLogin = new UserLogin();
            return View(_UserLogin);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(UserLogin userLogin, string ReturnUrl)
        {
            var user = _context.Admins.Where(u => u.Email == userLogin.Email && u.Password == userLogin.Password).FirstOrDefault();
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.AdminName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.AdminID)
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. Required when setting the 
                    // ExpireTimeSpan option of CookieAuthenticationOptions 
                    // set with AddCookie. Also required when setting 
                    // ExpiresUtc.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                // if there is returnUrl in params, return to the url
                if (String.IsNullOrEmpty(ReturnUrl))
                {
                    return RedirectToAction("Index", "Home");
                }

                return Redirect(ReturnUrl);
            }
            else
            {
                TempData.Add("Message", "Invalid Email or Password");
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> TempLogin(UserLogin userInfo, string ReturnUrl)
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

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserLogin userInfo, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = userInfo.Email, Email = userInfo.Email };
                var result = await _UserManager.CreateAsync(user, userInfo.Password);

                if (result.Succeeded)
                {
                    if (String.IsNullOrEmpty(ReturnUrl))
                    {
                        return RedirectToAction("Index", "Home");
                    }

                }

                return View();
            }
            return View();
        }
    }
}

