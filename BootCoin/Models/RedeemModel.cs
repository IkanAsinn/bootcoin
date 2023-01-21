using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models
{
    public class RedeemModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "nvarchar(50)")]
        public string TransactionID { get; set; }
        
        [DisplayName("Redeems Date")]
        [Column(TypeName = "Date")]
        public DateTime RedeemsDate { get; set; } = DateTime.Now;

        [DisplayName("Participant Id")]
        [Column(TypeName = "nvarchar(50)")]
        public string ParticipantID { get; set; }

        [DisplayName("Participant Name")]
        [Column(TypeName = "nvarchar(50)")]
        public string ParticipantName { get; set; }

        [DisplayName("Group")]
        [Column(TypeName = "nvarchar(50)")]
        public string Group { get; set; }

        [DisplayName("Coins Redeemed")]
        [Column(TypeName = "int")]
        public int CoinsRedeemed { get; set; }

    }
}
