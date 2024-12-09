using MediatR;
using Winecellar.Domain.Models;
using Winecellar.Application.Common.Interfaces;

namespace Winecellar.Application.Queries.Wines
{
    public class GetAllWinesQueryHandler : IRequestHandler<GetAllWinesQuery, IEnumerable<Wine>>
    {

        private readonly IWineRepository _wineRepository;
        public GetAllWinesQueryHandler(IWineRepository wineRepository)
        {
            _wineRepository = wineRepository;
        }
        public async Task<IEnumerable<Wine>> Handle(GetAllWinesQuery request,
            CancellationToken cancellationToken) => await _wineRepository.GetAllWines();
    }
}