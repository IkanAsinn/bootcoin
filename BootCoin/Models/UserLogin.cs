using System.ComponentModel.DataAnnotations;

namespace BootCoin.Models
{
    public class UserLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
