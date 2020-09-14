using Microsoft.EntityFrameworkCore;
using PantryMate.API.Entities;

namespace PantryMate.API
{
    public class PantryMateContext : DbContext
    {
        public PantryMateContext(DbContextOptions<PantryMateContext> options) : base(options)
        {

        }

        public DbSet<Account> Account { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<Profile> Profile { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
