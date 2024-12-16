using MediatR;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Application.Commands.Identity
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenDto>
    {
        private readonly IIdentityRepository _identityRepository;

        public LoginUserCommandHandler(IIdentityRepository identityRepository)
        {
            _identityRepository = identityRepository;
        }

        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            return new TokenDto
            {
                AccessToken = "accessToken",
                RefreshToken = "refreshToken"
                
            };
        }
    }
}
