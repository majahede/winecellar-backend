using MediatR;
using Winecellar.Application.Dtos.Wines;

namespace Winecellar.Application.Commands.Wines
{
    public record CreateWineCommand(CreateWineRequestDto wine) : IRequest;
}
