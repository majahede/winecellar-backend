using MediatR;

namespace Winecellar.Commands
{
    public record AddWineCommand(Wine wine) : IRequest;
}
