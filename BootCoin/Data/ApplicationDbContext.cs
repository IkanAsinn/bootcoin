using BootCoin.Models;
using BootCoin.Models.DBEntities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BootCoin.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Redeem> Redeems { get; set; }
        public DbSet<Participants> Participants { get; set; }
        public DbSet<Group> Group { get; set; }
    }
}
