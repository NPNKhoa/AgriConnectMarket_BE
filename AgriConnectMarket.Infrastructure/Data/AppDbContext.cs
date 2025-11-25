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
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<PreOrder> PreOrders { get; set; }

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

                b.HasMany(b => b.FavoriteFarms)
                    .WithOne(f => f.Customer)
                    .IsRequired()
                    .HasForeignKey(f => f.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
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

                f.HasMany(f => f.FavoriteFarms)
                    .WithOne(f => f.Farm)
                    .IsRequired()
                    .HasForeignKey(f => f.FarmId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Season>(s =>
            {
                s.ToTable("Seasons");

                s.HasKey(s => s.Id).HasName("SeasonId");

                s.HasOne(s => s.Farm)
                    .WithMany(f => f.Seasons)
                    .HasForeignKey(s => s.FarmId);

                s.HasOne(s => s.Product)
                    .WithMany(p => p.Seasons)
                    .HasForeignKey(s => s.ProductId);
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
                pb.ToTable("ProductBatches");

                pb.HasKey(pb => pb.Id).HasName("BatchId");

                pb.OwnsOne(o => o.BatchCode, bc =>
                {
                    bc.Property(x => x.Value).HasColumnName("BatchCode");
                });

                pb.HasOne(pb => pb.Season)
                    .WithMany(s => s.ProductBatches)
                    .HasForeignKey(pb => pb.SeasonId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);

                pb.Property(pb => pb.Price).HasPrecision(18, 2);
                pb.Property(pb => pb.TotalYield).HasPrecision(18, 4);
                pb.Property(pb => pb.AvailableQuantity).HasPrecision(18, 4);
            });

            modelBuilder.Entity<BatchCodeSequence>(b =>
            {
                b.ToTable("BatchCodeSequences");

                b.HasKey(x => x.Prefix);

                b.Property(x => x.Prefix).HasMaxLength(50).IsRequired();
                b.Property(x => x.LastNumber).IsRequired();
            });

            modelBuilder.Entity<FavoriteFarm>(f =>
            {
                f.ToTable("FavoriteFarms");

                f.HasKey(f => f.Id).HasName("FavoriteId");
            });

            modelBuilder.Entity<Cart>(c =>
            {
                c.ToTable("Carts");

                c.HasKey(c => c.Id).HasName("CartId");

                c.HasOne(c => c.Customer)
                    .WithOne(p => p.Cart)
                    .HasForeignKey<Cart>(c => c.CustomerId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CartItem>(ci =>
            {
                ci.ToTable("CartItems");

                ci.HasKey(ci => ci.Id).HasName("ItemId");

                ci.HasOne(ci => ci.Cart)
                    .WithMany(c => c.CartItems)
                    .HasForeignKey(ci => ci.CartId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<Order>(o =>
            {
                o.ToTable("Orders");

                o.HasKey(o => o.Id).HasName("OrderId");

                o.HasOne(o => o.Customer)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(o => o.CustomerId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<OrderItem>(i =>
            {
                i.ToTable("OrderItems");

                i.HasKey(i => i.Id).HasName("OrderItemId");

                i.HasOne(i => i.Order)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(i => i.OrderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<PreOrder>(i =>
            {
                i.ToTable("PreOrders");

                i.HasKey(i => i.OrderId).HasName("PreOrderId");

                i.HasOne(i => i.Order)
                    .WithOne(o => o.PreOrder)
                    .HasForeignKey<PreOrder>(i => i.OrderId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.Cascade);

                i.HasOne(p => p.Product)
                    .WithOne(p => p.PreOrder)
                    .HasForeignKey<PreOrder>(po => po.ProductId)
                    .IsRequired()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder.Entity<CareEventType>(t =>
            {
                t.ToTable("CareEventTypes");

                t.HasKey(t => t.Id).HasName("EventTypeId");
            });

            modelBuilder.Entity<CareEvent>(e =>
            {
                e.ToTable("CareEvents");

                e.HasKey(e => e.Id).HasName("EventId");

                e.HasOne(e => e.EventType)
                    .WithMany(t => t.CareEvents)
                    .HasForeignKey(e => e.EventTypeId)
                    .IsRequired();
            });
        }
    }
}

