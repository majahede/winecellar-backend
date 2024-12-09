using Winecellar.Application.Dtos.Wines;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Common.Interfaces 
{ 
    public interface IWineRepository
    {
        Task<IEnumerable<Wine>> GetAllWines();
        Task<Guid> CreateWine(CreateWineRequestDto request);

    }
}