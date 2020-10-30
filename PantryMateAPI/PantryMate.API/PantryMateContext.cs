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
        public DbSet<Pantry> Pantry { get; set; }
        public DbSet<Profile> Profile { get; set; }
        public DbSet<Item> Item { get; set; }
        public DbSet<PantryItem> PantryItem { get; set; }
        public DbSet<UnitOfMeasure> UnitOfMeasure { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PantryItem>()
                 .HasKey(e => new { e.PantryId, e.ItemId });

            modelBuilder.Entity<PantryItem>()
                .HasOne(e => e.Item)
                .WithMany(e => e.PantryItems)
                .HasForeignKey(e => e.ItemId);

            modelBuilder.Entity<PantryItem>()
                .HasOne(e => e.Pantry)
                .WithMany(e => e.PantryItems)
                .HasForeignKey(e => e.PantryId);

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
