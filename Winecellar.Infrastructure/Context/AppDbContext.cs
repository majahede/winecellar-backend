using Microsoft.EntityFrameworkCore;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Wine> Wines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Additional configurations
            Wine.Configure(modelBuilder);
        }
    }   
}
