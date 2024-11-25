using MediatR;
using Winecellar.Queries;

namespace Winecellar.Handlers
{
    public class GetWinesHandler : IRequestHandler<GetWinesQuery, IEnumerable<Wine>>
    {
        private readonly FakeDataStore _fakeDataStore;
        public GetWinesHandler(FakeDataStore fakeDataStore) => _fakeDataStore = fakeDataStore;
        public async Task<IEnumerable<Wine>> Handle(GetWinesQuery request,
            CancellationToken cancellationToken) => await _fakeDataStore.GetAllWines();
    }
}
