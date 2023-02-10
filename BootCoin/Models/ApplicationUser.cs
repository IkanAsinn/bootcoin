using Microsoft.AspNetCore.Identity;

namespace BootCoin.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string AdminName { get; set; }
        public string ImagePath { get; set; }
    }
}
