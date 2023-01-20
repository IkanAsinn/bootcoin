using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models
{
    public class UserLogin
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
