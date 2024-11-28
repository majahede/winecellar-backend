using Winecellar.Application.Models;

namespace Winecellar
{
    public class FakeDataStore
    {
        private static List<Wine> _wines;

        public FakeDataStore()
        {
            _wines = new List<Wine>
        {
            new Wine { Id = 1, Name = "Test Product 1" },
            new Wine { Id = 2, Name = "Test Product 2" },
            new Wine { Id = 3, Name = "Test Product 3" }
        };
        }

        public async Task AddWine(Wine wine)
        {
            _wines.Add(wine);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Wine>> GetAllWines() => await Task.FromResult(_wines);
    }
}
