using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;

namespace BootCoin.Models.DBEntities
{
    public class Participants
    {
        [Key]
        public string ParticipantID { get; set; }
        public string ParticipantName { get; set; }
        public string GroupID { get; set; }
        public Group Group { get; set; }
        public int CoinsObtained { get; set; }
        public int CoinsRedeemed { get; set; }
        public int TotalCoins { get; set; }

        public string ImagePath { get; set; }

        //public void calculateTotalCoins ()
        //{
        //    int totalCoins = CoinsObtained - CoinsRedeemed;
        //    if (totalCoins > 0)
        //    {
        //        TotalCoins = totalCoins;
        //    }
        //    else
        //    {
        //        TotalCoins = 0;
        //    }
        //}
    }
}
