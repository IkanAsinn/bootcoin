using BootCoin.Data;
using Microsoft.AspNetCore.Mvc;
using BootCoin.Models;


namespace BootCoin.Controllers
{
    public class FindUserController : Controller
    {
        public readonly ApplicationDbContext _context;

        public FindUserController(ApplicationDbContext context)
        {
            _context = context;
        }


        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Search(string ToFind)
        {
            var participants = _context.Bootcoin_Participants.ToList();
            if (ToFind == null || ToFind == "")
            {
                return PartialView("_Search", participants);
            }
            
            List<ParticipantsModel> participantsList = new List<ParticipantsModel>();
            foreach(var participant in participants)
            {
                if (participant.ParticipantName.ToUpper().Contains(ToFind.ToUpper()))
                {
                    participantsList.Add(participant);
                }
            }

            return PartialView("_Search", participantsList);
        }
    }
}
