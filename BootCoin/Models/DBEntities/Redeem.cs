using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class Redeem
    {
        [Key]
        public string TransactionID { get; set; }

        public DateTime TransactionDate { get; set; }

        public string ParticipantID { get; set; }
        public Participants Participant { get; set; }

        public int CoinsRedeemed { get; set; }
        public string PrizeName { get; set; }

    }
}
