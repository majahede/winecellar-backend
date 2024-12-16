namespace Winecellar.Application.Common.Interfaces
{
    public interface IIdentityRepository
    {
        Task<Guid> RegisterUser(string email, string password, string username);
    }
}
