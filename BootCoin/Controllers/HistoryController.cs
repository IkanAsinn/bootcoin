using BootCoin.Data;
using BootCoin.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
            // select all data from Bootcoin_Transaction table
            var transactions = _context.Bootcoin_Transactions.ToList();
            if (transactions != null)
            {
                List<TransactionModel> transactionList = new List<TransactionModel>();
                foreach (var transaction in transactions)
                {
                    var TransactionModel = new TransactionModel()
                    {
                        TransactionID = transaction.TransactionID,
                        TransactionDate = transaction.TransactionDate,
                        AdminID = transaction.AdminID,
                        AdminName = transaction.AdminName,
                        ParticipantName = transaction.ParticipantName,
                        Group = transaction.Group,
                        CoinsEarned = transaction.CoinsEarned
                    };
                    transactionList.Add(TransactionModel);
                }
                return PartialView("_History", transactionList);
            }

            return PartialView("_History", null);
        }

        public IActionResult Redeem()
        {
            var transactions = _context.Bootcoin_Redeems.ToList();
            if (transactions != null)
            {
                List<RedeemModel> RedeemsList = new List<RedeemModel>();
                foreach (var Redeems in transactions)
                {
                    var RedeemModel = new RedeemModel()
                    {
                        TransactionID = Redeems.TransactionID,
                        RedeemsDate = Redeems.RedeemsDate,
                        ParticipantID = Redeems.ParticipantID,
                        ParticipantName = Redeems.ParticipantName,
                        Group = Redeems.Group,
                        CoinsRedeemed = Redeems.CoinsRedeemed
                    };
                    RedeemsList.Add(RedeemModel);
                }
                return PartialView("_Redeem", RedeemsList);
            }

            return PartialView("_Redeem", null);
        }
    }
}
