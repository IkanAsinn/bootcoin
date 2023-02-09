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
            var users = _context.Participants
                .Join(_context.Group,
                p => p.GroupID,
                g => g.GroupID,
                (p, g) => new { p, g })
                .Where(pg => pg.p.GroupID == GroupID)
                .Select(pg => new UsersGroupModel()
                {
                    ParticipantID = pg.p.ParticipantID,
                    ParticipantName = pg.p.ParticipantName,
                    CoinsObtained = pg.p.CoinsObtained,
                    CoinsRedeemed = pg.p.CoinsRedeemed,
                    CoinsRemained = pg.p.TotalCoins,
                    Group = pg.g.GroupName,
                    TotalCoins = pg.g.TotalCoins,
                    TotalMember = pg.g.TotalMember,
                    Rank = pg.g.GroupRank
                }).ToList();
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
            var participant = _context.Participants.Where(p => p.ParticipantID == ParticipantID).FirstOrDefault();
            var admin = _context.Admin.Where(a => a.Email == User.Identity.Name).FirstOrDefault();
            var group = _context.Group.Where(g => g.GroupID == participant.GroupID).FirstOrDefault();

            Transaction newTransaction = new Transaction()
            {
                AdminID = admin.AdminID,
                Admin = admin,
                TransactionID = "ET" + (_context.Transactions.Count() + 1),
                ParticipantID = participant.ParticipantID,
                CoinsEarned = coinInput,
                TransactionDate = DateTime.Now
            };

            participant.CoinsObtained += coinInput;
            participant.TotalCoins += coinInput;
            _context.Participants.Update(participant);

            group.TotalCoins += coinInput;
            _context.Group.Update(group);

            _context.Transactions.Add(newTransaction);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}