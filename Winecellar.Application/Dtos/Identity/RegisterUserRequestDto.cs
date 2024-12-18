namespace Winecellar.Application.Dtos.Identity
{
    public class RegisterUserRequestDto
    {
        public string Password { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}
