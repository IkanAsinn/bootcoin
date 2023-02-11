using System.ComponentModel.DataAnnotations;

namespace BootCoin.Models
{
    public class AddUserModel
    {
        [Required]
        [Display(Name = "Participant Name")]
        public string ParticipantName { get; set; }
        [Required]
        [Display(Name = "Group ID")]
        public string GroupID { get; set; }
        
        public IFormFile Image { get; set; }
        public string GroupName { get; set; }
    }
}
