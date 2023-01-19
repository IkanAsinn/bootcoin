using BootCoin.Data;
using BootCoin.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace BootCoin.Controllers
{
    public class HistoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoryController(ApplicationDbContext context)
        {
            this._context = context;
        }
        public IActionResult Index()
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
                        TransactionId = transaction.TransactionID,
                        TransactionDate = transaction.TransactionDate,
                        AdminID = transaction.AdminID,
                        AdminName = transaction.AdminName,
                        ParticipantName = transaction.ParticipantName,
                        Group = transaction.Group,
                        CoinsEarned = transaction.CoinsEarned
                    };
                    transactionList.Add(TransactionModel);
                }
                return View(transactionList);
            }

            return View();
        }

        public IActionResult Redeem()
        {
            return View();
        }
    }
}
