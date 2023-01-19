using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class User
    {
        [Key]
        [Column(TypeName = "nvarchar(50)")]
        public string UserID { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Email { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Password { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Role { get; set; }
        [Column(TypeName = "nvarchar(50)")]
        public string Group { get; set; }
        [Column(TypeName = "int")]
        public int Coins { get; set; }
        
    }
}
