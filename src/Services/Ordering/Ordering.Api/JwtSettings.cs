namespace Ordering.Api
{
    public class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        // Token lifetime in minutes when issuing tokens
        public int ExpiresInMinutes { get; set; } = 60;
    }
}
