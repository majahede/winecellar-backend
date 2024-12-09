using MediatR;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Queries.Wines
{
    public record GetAllWinesQuery() : IRequest<IEnumerable<Wine>>;
}
