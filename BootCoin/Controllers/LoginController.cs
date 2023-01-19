using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using BootCoin.Models;
using BootCoin.Data;

namespace BootCoin.Controllers
{
    public class LoginController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            UserLogin _UserLogin = new UserLogin();
            return View(_UserLogin);
        }

        [HttpPost]
        public IActionResult Index(UserLogin _UserLogin)
        {
            ApplicationDbContext _context = new ApplicationDbContext();
            
        }
    }
}
