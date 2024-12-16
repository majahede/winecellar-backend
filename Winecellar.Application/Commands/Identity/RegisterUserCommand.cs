using MediatR;
using Winecellar.Application.Dtos.Identity;

namespace Winecellar.Application.Commands.Identity
{
    public record RegisterUserCommand(RegisterUserRequestDto user) : IRequest;
}
