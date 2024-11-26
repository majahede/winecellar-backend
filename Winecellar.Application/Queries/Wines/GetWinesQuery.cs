using MediatR;
using Winecellar.Application.Models;

namespace Winecellar.Application.Queries.Wines
{
    public record GetWinesQuery() : IRequest<IEnumerable<Wine>>;
}
