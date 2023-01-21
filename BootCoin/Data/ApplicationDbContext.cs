using BootCoin.Models;
using Microsoft.EntityFrameworkCore;

namespace BootCoin.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TransactionModel> Bootcoin_Transactions { get; set; }
        public DbSet<UserLogin> Admins { get; set; }
        public DbSet<RedeemModel> Bootcoin_Redeems { get; set; }
    }
}
