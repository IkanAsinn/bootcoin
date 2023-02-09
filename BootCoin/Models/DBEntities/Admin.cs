using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class Admin
    {
        [Key]
        public string AdminID { get; set; }

        public string AdminName { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
        
        public string? ImagePath { get; set; }
    }
}
