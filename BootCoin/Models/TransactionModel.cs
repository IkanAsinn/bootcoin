using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models
{
    public class TransactionModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "nvarchar(50)")]
        public string TransactionID { get; set; }
        
        [DisplayName("Transaction Date")]
        [Column(TypeName = "Date")]
        public DateTime TransactionDate { get; set; } = DateTime.Now;
        
        [DisplayName("Admin Id")]
        [Column(TypeName = "nvarchar(50)")]
        public string AdminID { get; set; }
        
        [DisplayName("Admin Name")]
        [Column(TypeName = "nvarchar(50)")]
        public string AdminName { get; set; }

        [DisplayName("Participant Id")]
        [Column(TypeName = "nvarchar(50)")]
        public string ParticipantID { get; set; }

        [DisplayName("Participant Name")]
        [Column(TypeName = "nvarchar(50)")]
        public string ParticipantName { get; set; }
        
        [DisplayName("Group")]
        [Column(TypeName = "nvarchar(50)")]
        public string Group { get; set; }
        
        [DisplayName("Coins Earned")]
        [Column(TypeName = "int")]
        public int CoinsEarned { get; set; }
    }
}
