using BootCoin.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BootCoin.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TransactionModel> Bootcoin_Transactions { get; set; }
        public DbSet<UserLogin> Admins { get; set; }
        public DbSet<RedeemModel> Bootcoin_Redeems { get; set; }
        public DbSet<ParticipantsModel> Bootcoin_Participants { get; set; }
    }
}
