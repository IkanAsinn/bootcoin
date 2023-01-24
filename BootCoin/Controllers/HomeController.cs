using BootCoin.Data;
using BootCoin.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BootCoin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            // if not logged in, redirect to /Login
            if (User.Identity.IsAuthenticated == false)
            {
                return RedirectToAction("Login", "Account");
            }
            // else, return view
            else
            {
                return View();
            }
        }

        [HttpGet]
        public IActionResult GetParticipants(string group)
        {
            var participants = _context.Bootcoin_Participants.Where(p => p.Group == group).ToList();
            List<UsersGroupModel> users = new List<UsersGroupModel>();
            foreach (var participant in participants)
            {
                var transactions = _context.Bootcoin_Transactions.Where(t => t.ParticipantID == participant.ParticipantID).ToList();
                var totalCoins = transactions.Sum(t => t.CoinsEarned);
                var redeemed = _context.Bootcoin_Redeems.Where(r => r.ParticipantID == participant.ParticipantID).ToList();
                var totalRedeemed = redeemed.Sum(r => r.CoinsRedeemed);
                UsersGroupModel user = new UsersGroupModel()
                {
                    ParticipantID = participant.ParticipantID,
                    ParticipantName = participant.ParticipantName,
                    CoinsObtained = totalCoins,
                    CoinsRedeemed = totalRedeemed,
                };
                user.CalculateCoinsRemained();
                users.Add(user);
            }
            return PartialView("_UserTable", users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}