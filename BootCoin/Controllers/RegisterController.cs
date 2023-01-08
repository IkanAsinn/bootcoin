using Microsoft.AspNetCore.Mvc;

namespace BootCoin.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
