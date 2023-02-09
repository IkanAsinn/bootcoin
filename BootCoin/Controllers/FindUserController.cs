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

            var participants = _context.Participants.ToList();
            foreach (var participant in participants)
            {
                FindUserModel user = new FindUserModel()
                {
                    ParticipantName = participant.ParticipantName,
                    Group = _context.Group.Where(g => g.GroupID == participant.GroupID).Select(g => g.GroupName).FirstOrDefault(),
                    TotalCoins = participant.TotalCoins
                };
                participantsList.Add(user);
            }
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
