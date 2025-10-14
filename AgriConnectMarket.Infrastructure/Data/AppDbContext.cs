using AgriConnectMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasKey(a => a.Id);
        }
    }
}
