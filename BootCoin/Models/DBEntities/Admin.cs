using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class Admin
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "nvarchar(50)")]
        public string AdminID { get; set; }

        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string AdminName { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Email { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        [Required]
        public string Password { get; set; }
        
    }
}
