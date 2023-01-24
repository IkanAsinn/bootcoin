namespace BootCoin.Models
{
    public class UsersGroupModel
    {
        public string ParticipantID { get; set; }
        public string ParticipantName { get; set; }
        public int CoinsObtained { get; set; }
        public int CoinsRedeemed { get; set; }
        public int CoinsRemained { get; set; }

        public void CalculateCoinsRemained()
        {
            int coins = this.CoinsObtained - this.CoinsRedeemed;
            if (coins < 0)
            {
                this.CoinsRemained = 0;
            }
            else
            {
                this.CoinsRemained = coins;
            }
        }
    }
}
