using BootCoin.Data;
using BootCoin.Models;
using BootCoin.Models.DBEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Diagnostics;
using System.Security.Claims;

namespace BootCoin.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        public static int groupTotalCoins;
        public static string groupName;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetParticipants(string GroupID)
        {
            var participants = _context.Participants.Where(p => p.GroupID == GroupID).ToList();
            List<UsersGroupModel> users = new List<UsersGroupModel>();
            
            foreach (var participant in participants)
            {
                UsersGroupModel user = new UsersGroupModel()
                {
                    ParticipantID = participant.ParticipantID,
                    ParticipantName = participant.ParticipantName,
                    CoinsObtained = participant.CoinsObtained,
                    CoinsRedeemed = participant.CoinsRedeemed,
                    CoinsRemained = participant.TotalCoins
                };
                users.Add(user);
            }
            return PartialView("_UserTable", users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult AddCoins(string id)
        {
            var participant = _context.Participants.Where(p => p.ParticipantID == id).FirstOrDefault();

            UsersGroupModel user = new UsersGroupModel()
            {
                ParticipantID = participant.ParticipantID,
                ParticipantName = participant.ParticipantName,
                CoinsObtained = 0,
                CoinsRedeemed = 0,
                CoinsRemained = 0
            };

            var result = JsonConvert.SerializeObject(user, Formatting.Indented);
            return PartialView("_AddCoinsModal", user);
        }

        [HttpPost]
        public IActionResult AddCoins(string ParticipantID, int coinInput)
        {
            Participants user = _context.Participants.Where(u => u.ParticipantID == ParticipantID).FirstOrDefault();
            Admin currentAdmin = _context.Admin.Where(a => a.AdminID == User.FindFirstValue(ClaimTypes.NameIdentifier)).FirstOrDefault();
            Group group = _context.Group.Where(g => g.GroupID == user.GroupID).FirstOrDefault();
            Transaction newTransaction = new Transaction()
            {
                TransactionID = "ET" + (_context.Transactions.Count() + 1),
                AdminID = currentAdmin.AdminID,
                ParticipantID = user.ParticipantID,
                CoinsEarned = coinInput
            };
            user.CoinsObtained += coinInput;
            user.TotalCoins += coinInput;
            group.TotalCoins += coinInput;
            _context.Update(group);
            _context.Update(user);
            _context.Transactions.Add(newTransaction);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}