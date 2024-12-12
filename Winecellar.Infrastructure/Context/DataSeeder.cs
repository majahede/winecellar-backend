
using Microsoft.EntityFrameworkCore;
using Winecellar.Domain.Models;

namespace Winecellar.Infrastructure.Context
{
    public static class DataSeeder
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var wines = new List<Wine>
            {
                new()
                {
                    Id = Guid.Parse("f5b3313d-5336-4663-82a3-dd2287ab6930"),
                    Name = "Wine 1",

                },
                new()
                {
                    Id = Guid.Parse("9ac1e64a-417f-4a3e-ab83-82478d2331e7"),
                    Name = "Wine 2"
                }
            };

            modelBuilder.Entity<Wine>().HasData(wines);
        }
    }
}
