using System.ComponentModel.DataAnnotations;

namespace BootCoin.Models
{
    public class ChangePhotoModel
    {
        [Required]
        public IFormFile Photo { get; set; }
    }
}
