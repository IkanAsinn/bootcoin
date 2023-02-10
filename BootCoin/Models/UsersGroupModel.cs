namespace BootCoin.Models
{
    public class UsersGroupModel
    {
        public string ParticipantID { get; set; }
        public string ParticipantName { get; set; }
        public int CoinsObtained { get; set; }
        public int CoinsRedeemed { get; set; }
        public int CoinsRemained { get; set; }
        public string Group { get; set; }
        public string GroupID { get; set; }
        public int TotalCoins { get; set; }
        public int TotalMember { get; set; }
        public int Rank { get; set; }
        public string ImagePath { get; set; }
    }
}
