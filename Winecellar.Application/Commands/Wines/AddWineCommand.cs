using MediatR;
using Winecellar.Application.Models;

namespace Winecellar.Application.Commands.Wines
{
    public record AddWineCommand(Wine wine) : IRequest;
}
