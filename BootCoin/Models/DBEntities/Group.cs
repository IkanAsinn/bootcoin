using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BootCoin.Models.DBEntities
{
    public class Group
    {
        [Key]
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public int GroupRank { get; set; }
        public int TotalMember { get; set; }
        public int TotalCoins { get; set; }
        public string ImagePath { get; set; }
        public List<Participants> Participants { get; set; }
    }
}
