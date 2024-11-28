using MediatR;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Queries.Wines
{
    public class GetWinesHandler : IRequestHandler<GetWinesQuery, IEnumerable<Wine>>
    {
        private readonly FakeDataStore _fakeDataStore;
        public GetWinesHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
        public async Task<IEnumerable<Wine>> Handle(GetWinesQuery request,
            CancellationToken cancellationToken) => await _fakeDataStore.GetAllWines();
    }
}
