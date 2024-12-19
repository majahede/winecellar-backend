
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

            var users = new List<User>
            {
                new()
                {
                    Id = Guid.Parse("80967cc2-9bef-4620-8a7d-15e55a1d2231"),
                    Username = "User1",
                    Email = "user1@mail.com",
                    Password = "123"

                },
                new()
                {
                    Id = Guid.Parse("34cc7e90-39de-41f3-b5db-a86b0c4e9008"),
                    Username = "User2",
                    Email = "user2@mail.com",
                    Password = "123"
                }
            };

            modelBuilder.Entity<User>().HasData(users);
            modelBuilder.Entity<Wine>().HasData(wines);
        }
    }
}
