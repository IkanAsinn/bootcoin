using Microsoft.AspNetCore.Mvc;

namespace BootCoin.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
