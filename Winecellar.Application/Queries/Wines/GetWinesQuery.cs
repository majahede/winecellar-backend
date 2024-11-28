using MediatR;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Queries.Wines
{
    public record GetWinesQuery() : IRequest<IEnumerable<Wine>>;
}
