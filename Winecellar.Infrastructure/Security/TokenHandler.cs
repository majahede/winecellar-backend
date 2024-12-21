using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Winecellar.Infrastructure.Security.Interfaces;

namespace Winecellar.Infrastructure.Security
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IOptions<TokenConfig> _tokenConfig;
        private readonly JwtSecurityTokenHandler _tokenHandler;

        public TokenHandler(IOptions<TokenConfig> tokenConfig)
        {
            _tokenConfig = tokenConfig;
            _tokenHandler = new JwtSecurityTokenHandler();
        }

        public string GenerateAccessToken(IEnumerable<Claim> userClaims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenConfig.Value.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _tokenConfig.Value.Issuer,
                audience: _tokenConfig.Value.Audience,
                claims: userClaims,
                expires: DateTime.Now.AddSeconds(_tokenConfig.Value.AccessTokenExpiration),
                signingCredentials: credentials);


            return _tokenHandler.WriteToken(token);
        }

        public string GenerateRefreshToken(string email)
        {
            throw new NotImplementedException();
        }
    }
}
