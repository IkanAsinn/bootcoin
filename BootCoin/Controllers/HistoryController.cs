using BootCoin.Data;
using BootCoin.Models;
using BootCoin.Models.DBEntities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BootCoin.Controllers
{
    [Authorize]
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details()
        {
            // select all data from Transaction table
            var transactions = _context.Transactions.ToList();
            if (transactions != null)
            {
                List<TransactionModel> transactionList = new List<TransactionModel>();
                foreach (var transaction in transactions)
                {
                    Participants participant = _context.Participants.Where(p => p.ParticipantID == transaction.ParticipantID).FirstOrDefault();
                    var TransactionModel = new TransactionModel()
                    {
                        TransactionID = transaction.TransactionID,
                        TransactionDate = transaction.TransactionDate,
                        AdminID = transaction.AdminID,
                        AdminName = _context.Admin.Where(a => a.AdminID == transaction.AdminID).FirstOrDefault().AdminName,
                        ParticipantID = transaction.ParticipantID,
                        ParticipantName = participant.ParticipantName,
                        CoinsEarned = transaction.CoinsEarned,
                        Group = _context.Group.Where(g => g.GroupID == participant.GroupID).FirstOrDefault().GroupName
                    };
                    transactionList.Add(TransactionModel);
                }
                return PartialView("_History", transactionList);
            }

            return PartialView("_History", null);
        }

        public IActionResult Redeem()
        {
            var transactions = _context.Redeems.ToList();
            if (transactions != null)
            {
                List<RedeemModel> RedeemsList = new List<RedeemModel>();
                foreach (var Redeems in transactions)
                {
                    Participants participant = _context.Participants.Where(p => p.ParticipantID == Redeems.ParticipantID).FirstOrDefault();                    var RedeemModel = new RedeemModel()
                    {
                        TransactionID = Redeems.TransactionID,
                        RedeemsDate = Redeems.TransactionDate,
                        ParticipantID = Redeems.ParticipantID,
                        ParticipantName = participant.ParticipantName,
                        CoinsRedeemed = Redeems.CoinsRedeemed,
                        Group = _context.Group.Where(g => g.GroupID == participant.GroupID).FirstOrDefault().GroupName
                    };
                    RedeemsList.Add(RedeemModel);
                }
                return PartialView("_Redeem", RedeemsList);
            }

            return PartialView("_Redeem", null);
        }
    }
}
