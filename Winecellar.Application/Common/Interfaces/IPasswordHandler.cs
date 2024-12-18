namespace Winecellar.Application.Common.Interfaces
{
    public interface IPasswordHandler
    {
        string HashPassword(string password);
        bool VerifyPassword(string providedPassword, string hashedPassword);
    }
}
