using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BootCoin.Models
{
    public class TransactionModel
    {
        public string TransactionId { get; set; }
        [DisplayName("Transaction Date")]
        [DataType(DataType.Date)]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        [DisplayName("Admin Id")]
        public string AdminID { get; set; }
        [DisplayName("Admin Name")]
        public string AdminName { get; set; }
        [DisplayName("Participant Name")]
        public string ParticipantName { get; set; }
        [DisplayName("Group")]
        public string Group { get; set; }
        [DisplayName("Coins Earned")]
        public int CoinsEarned { get; set; }
    }
}
