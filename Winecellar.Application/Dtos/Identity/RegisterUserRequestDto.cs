namespace Winecellar.Application.Dtos.Identity
{
    public class RegisterUserRequestDto
    {
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
    }
}
