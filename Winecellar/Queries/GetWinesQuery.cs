using MediatR;

namespace Winecellar.Queries
{
    public record GetWinesQuery() : IRequest<IEnumerable<Wine>>;
}
