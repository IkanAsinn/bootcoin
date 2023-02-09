using BootCoin.Data;
using BootCoin.Models.DBEntities;
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
            // select all data from Transaction table
            var transactions = _context.Transactions.ToList();
            if (transactions != null)
            {
                List<Transaction> transactionList = new List<Transaction>();
                foreach (var transaction in transactions)
                {
                    var TransactionModel = new Transaction()
                    {
                        TransactionID = transaction.TransactionID,
                        TransactionDate = transaction.TransactionDate,
                        AdminID = transaction.AdminID,
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
            var transactions = _context.Redeems.ToList();
            if (transactions != null)
            {
                List<Redeem> RedeemsList = new List<Redeem>();
                foreach (var Redeems in transactions)
                {
                    var RedeemModel = new Redeem()
                    {
                        TransactionID = Redeems.TransactionID,
                        ParticipantID = Redeems.ParticipantID,
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
