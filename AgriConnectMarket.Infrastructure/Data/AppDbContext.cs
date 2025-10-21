using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options, IDateTimeProvider _dateTimeProvider) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            // Auditing: populate CreatedAt/ModifiedAt
            foreach (var entry in ChangeTracker.Entries<IAuditableEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = _dateTimeProvider.UtcNow;
                    // optionally set CreatedBy from current user context (infrastructure)
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = _dateTimeProvider.UtcNow;
                    // optionally set ModifiedBy
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // map User
            modelBuilder.Entity<Account>(b =>
            {
                b.ToTable("Accounts");
                b.HasKey(u => u.Id);
                b.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                b.HasIndex(u => u.UserName).IsUnique();
                b.Property(u => u.Password).IsRequired().HasMaxLength(100);
                // audit fields
                b.Property(u => u.CreatedAt).IsRequired();
                b.Property(u => u.CreatedBy).HasMaxLength(100);
                b.Property(u => u.UpdatedAt);
                b.Property(u => u.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<Profile>(b =>
            {
                b.ToTable("Profiles");
                b.HasKey(p => p.Id);

                b.HasOne(p => p.Account)
                    .WithOne(a => a.Profile)
                    .HasForeignKey<Profile>(p => p.AccountId)
                    .IsRequired();
            });
        }
    }
}

