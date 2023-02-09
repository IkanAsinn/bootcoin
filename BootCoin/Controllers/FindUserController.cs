using BootCoin.Data;
using Microsoft.AspNetCore.Mvc;
using BootCoin.Models;


namespace BootCoin.Controllers
{
    public class FindUserController : Controller
    {
        public readonly ApplicationDbContext _context;
        public List<UsersGroupModel> participantsList = new List<UsersGroupModel>();


        public FindUserController(ApplicationDbContext context)
        {
            _context = context;

            var participants = _context.Participants.ToList();
            foreach (var participant in participants)
            {
                var transactions = _context.Transactions.Where(t => t.ParticipantID == participant.ParticipantID).ToList();
                var redeemed = _context.Redeems.Where(r => r.ParticipantID == participant.ParticipantID).ToList();
                UsersGroupModel user = new UsersGroupModel()
                {
                    ParticipantID = participant.ParticipantID,
                    ParticipantName = participant.ParticipantName,
                    CoinsObtained = transactions.Sum(t => t.CoinsEarned),
                    CoinsRedeemed = redeemed.Sum(r => r.CoinsRedeemed),
                };
                user.CalculateCoinsRemained();
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
                List<UsersGroupModel> users = new List<UsersGroupModel>();
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
