using Winecellar.Application.Common.Interfaces;

namespace Winecellar.Infrastructure.Security
{
    public class PasswordHandler : IPasswordHandler
    {

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string providedPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
}