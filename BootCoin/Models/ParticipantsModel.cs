using System.ComponentModel.DataAnnotations;

namespace BootCoin.Models
{
    public class ParticipantsModel
    {
        [Key]
        public string ParticipantID { get; set; }
        [Required]
        public string ParticipantName { get; set; }
        public string ParticipantGender { get; set; }
        public string Group { get; set; }
        public string CareerPath { get; set; }
        public string Status { get; set; }

    }
}
