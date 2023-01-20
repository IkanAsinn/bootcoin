using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BootCoin.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.Name = User.FindFirstValue(ClaimTypes.Name);
            ViewBag.Email = User.FindFirstValue(ClaimTypes.Email);
            ViewBag.Role = User.FindFirstValue(ClaimTypes.Role);
            return View();
        }
    }
}
