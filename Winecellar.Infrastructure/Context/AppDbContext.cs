using Microsoft.EntityFrameworkCore;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Context
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Wine> Wines { get; set; }
        public DbSet<Wine> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            DataSeeder.SeedData(modelBuilder);

            Wine.Configure(modelBuilder);
        }
    }   
}
