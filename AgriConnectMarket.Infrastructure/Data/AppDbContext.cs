using AgriConnectMarket.Domain.Entities;
using AgriConnectMarket.Infrastructure.Entities;
using AgriConnectMarket.SharedKernel.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AgriConnectMarket.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions options, IDateTimeProvider _dateTimeProvider) : DbContext(options)
    {
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductBatch> ProductBatches { get; set; }
        public DbSet<BatchCodeSequence> BatchCodeSequences { get; set; }

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

                b.HasKey(u => u.Id).HasName("AccountId");
                b.Property(u => u.UserName).IsRequired().HasMaxLength(50);
                b.HasIndex(u => u.UserName).IsUnique();
                b.Property(u => u.Password).IsRequired().HasMaxLength(100);
                b.Property(u => u.CreatedAt).IsRequired();
                b.Property(u => u.CreatedBy).HasMaxLength(100);
                b.Property(u => u.UpdatedAt);
                b.Property(u => u.UpdatedBy).HasMaxLength(100);
            });

            modelBuilder.Entity<Profile>(b =>
            {
                b.ToTable("Profiles");
                b.HasKey(p => p.Id).HasName("ProfileId");

                b.HasOne(p => p.Account)
                    .WithOne(a => a.Profile)
                    .HasForeignKey<Profile>(p => p.AccountId)
                    .IsRequired();
            });

            modelBuilder.Entity<Address>(a =>
            {
                a.ToTable("Addresses");

                a.HasKey(a => a.Id).HasName("AddressId");
            });

            modelBuilder.Entity<Farm>(f =>
            {
                f.ToTable("Farms");
                f.HasKey(f => f.Id);

                f.HasOne(f => f.Farmer)
                    .WithOne(a => a.Farm)
                    .HasForeignKey<Farm>(f => f.FarmerId)
                    .IsRequired();

                f.HasOne(f => f.Address)
                    .WithOne(a => a.Farm)
                    .HasForeignKey<Farm>(f => f.AddressId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

                f.HasMany(f => f.Seasons)
                    .WithOne(s => s.Farm)
                    .HasForeignKey(s => s.FarmId);
            });

            modelBuilder.Entity<Season>(s =>
            {
                s.ToTable("Seasons");

                s.HasKey(s => s.Id).HasName("SeasonId");
            });

            modelBuilder.Entity<Category>(c =>
            {
                c.ToTable("Categories");

                c.HasKey(c => c.Id).HasName("CategoryId");
            });

            modelBuilder.Entity<Product>(p =>
            {
                p.ToTable("Products");

                p.HasKey(p => p.Id).HasName("ProductId");

                p.HasOne(p => p.Category)
                    .WithMany(c => c.Products)
                    .HasForeignKey(p => p.CategoryId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<ProductBatch>(pb =>
            {
                pb.ToTable("ProductBatchs");

                pb.HasKey(pb => pb.Id).HasName("BatchId");

                pb.OwnsOne(o => o.BatchCode, bc =>
                {
                    bc.Property(x => x.Value).HasColumnName("BacthCode");
                });

                pb.HasOne(pb => pb.Season)
                    .WithMany(s => s.ProductBatches)
                    .HasForeignKey(pb => pb.SeasonId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<BatchCodeSequence>(b =>
            {
                b.ToTable("OrderCodeSequences");

                b.HasKey(x => x.Prefix);

                b.Property(x => x.Prefix).HasMaxLength(50).IsRequired();
                b.Property(x => x.LastNumber).IsRequired();
            });
        }
    }
}

