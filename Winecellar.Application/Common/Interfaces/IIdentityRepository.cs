using Winecellar.Domain.Models;

namespace Winecellar.Application.Common.Interfaces
{
    public interface IIdentityRepository
    {
        Task<User?> GetByUsernameOrEmail(string loginInput);
        Task<Guid> RegisterUser(string email, string password, string username);
        Task StoreRefreshToken(string refreshToken, Guid id);
    }
}
