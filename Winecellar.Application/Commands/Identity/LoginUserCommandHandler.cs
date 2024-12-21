using MediatR;
using System.Security.Claims;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Application.Dtos.Token;
using Winecellar.Infrastructure.Security.Interfaces;

namespace Winecellar.Application.Commands.Identity
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, TokenDto>
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IPasswordHandler _passwordHandler;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(IIdentityRepository identityRepository, IPasswordHandler passwordHandlerI, ITokenHandler tokenHandler)
        {
            _identityRepository = identityRepository;
            _passwordHandler = passwordHandlerI;
            _tokenHandler = tokenHandler;
        }

        public async Task<TokenDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityRepository.GetByUsernameOrEmail(request.user.LoginInput);

            _ = user != null ? true : throw new UnauthorizedAccessException("Invalid credentials");

            _ = _passwordHandler.VerifyPassword(request.user.Password, user.Password)
                ? true
                : throw new UnauthorizedAccessException("Invalid credentials");

            var loginClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email)
                };

            var accessToken = _tokenHandler.GenerateAccessToken(loginClaims);
            var refreshToken = _tokenHandler.GenerateRefreshToken(user.Email);

            await _identityRepository.StoreRefreshToken(refreshToken, user.Id);


            return new TokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
