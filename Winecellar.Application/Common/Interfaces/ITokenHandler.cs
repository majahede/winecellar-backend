using System.Security.Claims;

namespace Winecellar.Infrastructure.Security.Interfaces
{
    public interface ITokenHandler
    {
        string GenerateAccessToken(IEnumerable<Claim> userClaims);
        string GenerateRefreshToken(string email);
    }
}
