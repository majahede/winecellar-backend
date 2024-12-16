using MediatR;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Application.Commands.Identity
{
    public record LoginUserCommand(LoginUserRequestDto user) : IRequest<TokenDto>;
    
}
