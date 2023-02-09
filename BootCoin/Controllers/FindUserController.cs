using BootCoin.Data;
using Microsoft.AspNetCore.Mvc;
using BootCoin.Models;
using BootCoin.Models.DBEntities;

namespace BootCoin.Controllers
{
    public class FindUserController : Controller
    {
        public readonly ApplicationDbContext _context;
        public List<FindUserModel> participantsList = new List<FindUserModel>();


        public FindUserController(ApplicationDbContext context)
        {
            _context = context;

            participantsList = _context.Participants
                .Join(_context.Group,
                p => p.GroupID,
                g => g.GroupID,
                (p, g) => new { p, g })
                .Select(pg => new FindUserModel()
                {
                    ParticipantName = pg.p.ParticipantName,
                    Group = pg.g.GroupName,
                    TotalCoins = pg.p.TotalCoins
                }).ToList();
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string ToFind)
        {
            if (!string.IsNullOrEmpty(ToFind))
            {
                List<FindUserModel> users = new List<FindUserModel>();
                foreach (var participant in participantsList)
                {
                    if (participant.ParticipantName.ToUpper().Contains(ToFind.ToUpper()))
                    {
                        users.Add(participant);
                    }
                }
                return PartialView("_Search", users);
            }

            return PartialView("_Search", participantsList);
        }
    }
}
