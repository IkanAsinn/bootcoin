using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class Bootcoin_Transactions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "nvarchar(50)")]
        public string TransactionID { get; set; }
        [Column(TypeName = "date")]
        public DateTime TransactionDate { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        public string AdminID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string AdminName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string ParticipantName { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Group { get; set; }
        [Column(TypeName = "int")]
        public int CoinsEarned { get; set; }
    }
}
