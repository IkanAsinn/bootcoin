using BootCoin.Data;
using BootCoin.Models;
using BootCoin.Models.DBEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BootCoin.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProfileController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager, 
            ApplicationDbContext context,
            IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _context = context;
            _signInManager = signInManager;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel passwords)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, passwords.OldPassword, passwords.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    foreach (var error in changePasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return RedirectToAction("Index", "Profile");
                }

                Admin admin = _context.Admin.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
                admin.Password = passwords.NewPassword;
                _context.Admin.Update(admin);
                _context.SaveChanges();
                await _signInManager.RefreshSignInAsync(user);

                return RedirectToAction("Index", "Profile");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangeInfo(ChangeInfoModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                Admin currentAdmin = _context.Admin.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
                if (user == null)
                {
                    return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
                }

                user.AdminName = userInfo.Name;
                user.UserName = userInfo.Email;
                user.Email = userInfo.Email;

                await _userManager.UpdateAsync(user);

                currentAdmin.AdminName = userInfo.Name;
                currentAdmin.Email = userInfo.Email;

                _context.Admin.Update(currentAdmin);
                _context.SaveChanges();

                await _signInManager.RefreshSignInAsync(user);
                return RedirectToAction("Index", "Profile");
            }
            return RedirectToAction("Index", "Profile");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePhoto(ChangePhotoModel userPhoto)
        {
            if (ModelState.IsValid)
            {
                string folder = "images/user-photo/";
                folder += Guid.NewGuid().ToString() + userPhoto.Photo.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await userPhoto.Photo.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                var user = await _userManager.GetUserAsync(User);

                string oldImage = Path.Combine(_webHostEnvironment.WebRootPath, user.ImagePath);

                if (System.IO.File.Exists(oldImage))
                {
                    System.IO.File.Delete(oldImage);
                }

                user.ImagePath = "/" + folder;
                await _userManager.UpdateAsync(user);

                var admin = _context.Admin.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
                admin.ImagePath = "/" + folder;
                _context.Admin.Update(admin);
                _context.SaveChanges();

                await _signInManager.RefreshSignInAsync(user);

                return RedirectToAction("Index", "Profile");
            }

            return RedirectToAction("Index", "Profile");
        }
    }
}
