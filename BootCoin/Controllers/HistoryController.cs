using Microsoft.AspNetCore.Mvc;

namespace BootCoin.Controllers
{
    public class HistoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Redeem()
        {
            return View();
        }
    }
}
