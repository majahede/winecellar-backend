using MediatR;
using Winecellar.Application.Dtos.Wines;
using Winecellar.Domain.Models;

namespace Winecellar.Application.Commands.Wines
{
    public record CreateWineCommand(CreateWineRequestDto wine) : IRequest;
}
