namespace Winecellar.Infrastructure.Security
{

    public class TokenConfig
    {
        public string Audience { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string SecretKey { get; set; } = null!;
        public long AccessTokenExpiration { get; set; }
    }
}