using BootCoin.Data;
using Microsoft.AspNetCore.Mvc;
using BootCoin.Models;
using BootCoin.Models.DBEntities;
using System.Dynamic;
using Microsoft.IdentityModel.Tokens;

namespace BootCoin.Controllers
{
    public class FindUserController : Controller
    {
        public readonly ApplicationDbContext _context;
        public List<FindUserModel> participantsList = new List<FindUserModel>();
        public List<FindUserModel> result = new List<FindUserModel>();

        public FindUserController(ApplicationDbContext context)
        {
            _context = context;
            var participants = _context.Participants.ToList();
            foreach (var participant in participants)
            {
                FindUserModel p = new FindUserModel();
                p.ParticipantName = participant.ParticipantName;
                p.Group = _context.Group.Where(g => g.GroupID == participant.GroupID).Select(g => g.GroupName).FirstOrDefault();
                p.TotalCoins = participant.TotalCoins;
                participantsList.Add(p);
            }

        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Search(string ToFind, int page = 1)
        {
            if (page < 1)
            {
                page = 1;
            }
            List<FindUserModel> users = new List<FindUserModel>();
            result.Clear();

            if (!string.IsNullOrEmpty(ToFind))
            {
                users = getUsers(ToFind);
            }


            int pCount = (!users.IsNullOrEmpty()) ? users.Count() : participantsList.Count();
            int pageSize = (!users.IsNullOrEmpty() && users.Count() < 5) ? users.Count() : 5;
            Pager pager = new Pager(pCount, page, pageSize);
            int pSkip = (page - 1) * pageSize;

            if (!users.IsNullOrEmpty())
            {
                for (int i = pSkip; i < pSkip + pageSize; i++)
                {
                    result.Add(users[i]);
                }
            }
            else
            {
                for (int i = pSkip; i < pSkip + pageSize; i++)
                {
                    result.Add(participantsList[i]);
                }
            }

            dynamic mymodel = new ExpandoObject();
            mymodel.pager = pager;
            mymodel.participantsList = result;

            return PartialView("_Search", mymodel);
        }

        public List<FindUserModel> getUsers(string ToFind)
        {
            List<FindUserModel> users = new List<FindUserModel>();
            foreach (var participant in participantsList)
            {
                if (participant.ParticipantName.ToUpper().Contains(ToFind.ToUpper()))
                {
                    users.Add(participant);
                }
            }
            return users;
        }
    }
}
