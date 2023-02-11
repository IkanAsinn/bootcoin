using BootCoin.Data;
using BootCoin.Models;
using BootCoin.Models.DBEntities;
using ClosedXML.Excel;
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
                List<TransactionModel> transactionList = _context.Transactions
                        .Join(_context.Participants,
                                tr => tr.ParticipantID,
                                p => p.ParticipantID,
                                (tr, p) => new { tr, p })
                        .Join(_context.Group,
                        trp => trp.p.GroupID,
                                g => g.GroupID,
                                (trp, g) => new { trp, g })
                        .Join(_context.Admin,
                        trpg => trpg.trp.tr.AdminID,
                                a => a.AdminID,
                                (trpg, a) => new { trpg, a })
                        .Select(trpga => new TransactionModel
                        {
                            TransactionID = trpga.trpg.trp.tr.TransactionID,
                            TransactionDate = trpga.trpg.trp.tr.TransactionDate,
                            ParticipantID = trpga.trpg.trp.tr.ParticipantID,
                            ParticipantName = trpga.trpg.trp.p.ParticipantName,
                            Group = trpga.trpg.g.GroupName,
                            CoinsEarned = trpga.trpg.trp.tr.CoinsEarned,
                            AdminName = trpga.a.AdminName
                        }).ToList();
                
                return PartialView("_History", transactionList);
            }

            return PartialView("_History", null);
        }

        public IActionResult Redeem()
        {
            var transactions = _context.Redeems.ToList();
            if (transactions != null)
            {
                List<RedeemModel> RedeemsList = _context.Redeems
                    .Join(_context.Participants,
                    re => re.ParticipantID,
                    p => p.ParticipantID,
                    (re, p) => new { re, p })
                    .Join(_context.Group,
                    rep => rep.p.GroupID,
                    g => g.GroupID,
                    (rep, g) => new { rep, g })
                    .Select(repg => new RedeemModel()
                    {
                        TransactionID = repg.rep.re.TransactionID,
                        ParticipantID = repg.rep.re.ParticipantID,
                        ParticipantName = repg.rep.p.ParticipantName,
                        Group = repg.g.GroupName,
                        RedeemsDate = repg.rep.re.TransactionDate,
                        PrizeName = repg.rep.re.PrizeName,
                        CoinsRedeemed = repg.rep.re.CoinsRedeemed
                    }).ToList();
                return PartialView("_Redeem", RedeemsList);
            }

            return PartialView("_Redeem", null);
        }

        public IActionResult ExportExcel()
        {
            using(var workbook = new XLWorkbook())
            {
                var worksheet1 = workbook.Worksheets.Add("Transaction");
                var currentRow = 1;
                List<Transaction> transactions = _context.Transactions.ToList();

                #region Header
                worksheet1.Cell(currentRow, 1).Value = "ID";
                worksheet1.Cell(currentRow, 2).Value = "Date";
                worksheet1.Cell(currentRow, 3).Value = "AdminID";
                worksheet1.Cell(currentRow, 4).Value = "ParticipantID";
                worksheet1.Cell(currentRow, 5).Value = "CoinsEarned";
                #endregion

                #region Body
                foreach (var transaction in transactions)
                {
                    currentRow++;
                    worksheet1.Cell(currentRow, 1).Value = transaction.TransactionID;
                    worksheet1.Cell(currentRow, 2).Value = transaction.TransactionDate;
                    worksheet1.Cell(currentRow, 3).Value = transaction.AdminID;
                    worksheet1.Cell(currentRow, 4).Value = transaction.ParticipantID;
                    worksheet1.Cell(currentRow, 5).Value = transaction.CoinsEarned;
                }
                #endregion

                var worksheet2 = workbook.Worksheets.Add("Redeem");
                currentRow = 1;
                List<Redeem> redeems = _context.Redeems.ToList();

                #region Header
                worksheet2.Cell(currentRow, 1).Value = "ID";
                worksheet2.Cell(currentRow, 2).Value = "Date";
                worksheet2.Cell(currentRow, 3).Value = "ParticipantID";
                worksheet2.Cell(currentRow, 4).Value = "Coins Redeemed";
                worksheet2.Cell(currentRow, 5).Value = "Prize Name";
                #endregion

                #region
                foreach(var redeem in redeems)
                {
                    currentRow++;
                    worksheet2.Cell(currentRow, 1).Value = redeem.TransactionID;
                    worksheet2.Cell(currentRow, 2).Value = redeem.TransactionDate;
                    worksheet2.Cell(currentRow, 3).Value = redeem.ParticipantID;
                    worksheet2.Cell(currentRow, 4).Value = redeem.CoinsRedeemed;
                    worksheet2.Cell(currentRow, 5).Value = redeem.PrizeName;
                }
                #endregion

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();

                    return File(content,
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "History.xlsx"
                        );
                }
            }
        }
    }
}
