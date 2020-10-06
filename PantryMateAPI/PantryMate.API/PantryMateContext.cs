using Microsoft.EntityFrameworkCore;
using PantryMate.API.Controllers;
using PantryMate.API.Entities;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PantryMate.API
{
    public class PantryMateContext : DbContext
    {
        public PantryMateContext(DbContextOptions<PantryMateContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<InventoryItem> InventoryItem { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InventoryItem>()
                 .HasKey(e => new { e.InventoryId, e.ItemId });

            modelBuilder.Entity<InventoryItem>()
                .HasOne(e => e.Item)
                .WithMany(e => e.InventoryItems)
                .HasForeignKey(e => e.ItemId);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(e => e.Inventory)
                .WithMany(e => e.InventoryItems)
                .HasForeignKey(e => e.InventoryId);

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries().Where(e => e.Entity is BaseEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((BaseEntity)entityEntry.Entity).UpdatedOn = DateTime.Now;

                if (entityEntry.State == EntityState.Added)
                {
                    ((BaseEntity)entityEntry.Entity).CreatedOn = DateTime.Now;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
