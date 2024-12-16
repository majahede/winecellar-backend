namespace Winecellar.Infrastructure.Security
{
    public static class PasswordHandler
    {

        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public static bool VerifyPassword(string providedPassword, string hashedPassword) => BCrypt.Net.BCrypt.Verify(providedPassword, hashedPassword);
    }
    
}
