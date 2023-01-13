using Microsoft.AspNetCore.Mvc;

namespace BootCoin.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
