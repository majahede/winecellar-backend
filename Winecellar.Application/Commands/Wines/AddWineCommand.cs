using MediatR;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Commands.Wines
{
    public record AddWineCommand(Wine wine) : IRequest;
}
