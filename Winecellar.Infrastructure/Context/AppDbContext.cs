using Microsoft.EntityFrameworkCore;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Context
{
    public class AppDbContext: DbContext
    {
        public DbSet<Wine> Wines { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DataSeeder.SeedData(modelBuilder);

            Wine.Configure(modelBuilder);
            User.Configure(modelBuilder);
            RefreshToken.Configure(modelBuilder);
        }
    }   
}
