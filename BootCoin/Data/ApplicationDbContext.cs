using BootCoin.Models.DBEntities;
using Microsoft.EntityFrameworkCore;

namespace BootCoin.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Bootcoin_Transactions> Bootcoin_Transactions { get; set; }
        public DbSet<Admin> Admins { get; set; }
    }
}
