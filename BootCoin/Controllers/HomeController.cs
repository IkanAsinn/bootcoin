using BootCoin.Data;
using BootCoin.Models;
using BootCoin.Models.DBEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IWebHostEnvironment _webHostEnvironment;
        public static int groupTotalCoins;
        public static string groupName;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
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
                    Rank = pg.g.GroupRank,
                    ImagePath = pg.p.ImagePath,
                    GroupID = pg.p.GroupID
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

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserModel user)
        {
            if (!string.IsNullOrEmpty(user.ParticipantName) && !string.IsNullOrEmpty(user.GroupID))
            {
                string folder = "images/user-photo/";
                folder += Guid.NewGuid().ToString() + user.Image.FileName;
                string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folder);

                await user.Image.CopyToAsync(new FileStream(serverFolder, FileMode.Create));

                var group = _context.Group.Where(g => g.GroupID == user.GroupID).FirstOrDefault();

                Participants newParticipant = new Participants()
                {
                    ParticipantID = group.GroupName + (_context.Participants.Count() + 1).ToString("D2"),
                    ParticipantName = user.ParticipantName,
                    CoinsObtained = 0,
                    CoinsRedeemed = 0,
                    Group = group,
                    GroupID = group.GroupID,
                    ImagePath = "/" + folder,
                    TotalCoins = 0
                };

                group.TotalMember += 1;
                _context.Group.Update(group);

                _context.Participants.Add(newParticipant);
                _context.SaveChanges();

                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Index", "Home");
        }
    }
}