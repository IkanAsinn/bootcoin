using System.ComponentModel.DataAnnotations;

namespace BootCoin.Models
{
    public class ChangeInfoModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
