using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class Transaction
    {
        [Key]
        public string TransactionID { get; set; }

        public DateTime TransactionDate { get; set; }

        public string AdminID { get; set; }
        public Admin Admin { get; set; }

        public string ParticipantID { get; set; }

        public int CoinsEarned { get; set; }
    }
}
