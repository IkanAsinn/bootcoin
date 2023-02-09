using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models
{
    public class RedeemModel
    {
        public string TransactionID { get; set; }
        
        [DisplayName("Redeems Date")]
        public DateTime RedeemsDate { get; set; } = DateTime.Now;

        public string ParticipantID { get; set; }

        [DisplayName("Participant Name")]
        public string ParticipantName { get; set; }

        [DisplayName("Group")]
        public string Group { get; set; }

        [DisplayName("Coins Redeemed")]
        public int CoinsRedeemed { get; set; }
        
        [DisplayName("Prize Name")]
        public string PrizeName { get; set; }

    }
}
