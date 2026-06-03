using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.Infrastructure
{
    public class JwtSettings
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public int ExpiresInMinutes { get; set; } = 60;
        public int RefreshExpiresInMinutes { get; set; } = 60 * 24 * 7; // default 7 days
    }

    public class JwtTokenService : Identity.Application.Contracts.IJwtService
    {
        private readonly JwtSettings _settings;

        public JwtTokenService(IOptions<JwtSettings> options)
        {
            _settings = options?.Value ?? throw new ArgumentNullException(nameof(options));
        }

        public string CreateAccessToken(Identity.Domain.ApplicationUser user, IEnumerable<string>? roles = null)
        {
            if (string.IsNullOrEmpty(_settings.Key))
                throw new InvalidOperationException("JWT key is not configured");

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName ?? string.Empty),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName ?? string.Empty)
            };

            foreach (var role in roles ?? Array.Empty<string>())
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var now = DateTime.UtcNow;
            var expires = now.AddMinutes(_settings.ExpiresInMinutes <= 0 ? 60 : _settings.ExpiresInMinutes);

            var token = new JwtSecurityToken(
                issuer: _settings.Issuer,
                audience: _settings.Audience,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            var random = new byte[64];
            using var rng = System.Security.Cryptography.RandomNumberGenerator.Create();
            rng.GetBytes(random);
            return Convert.ToBase64String(random);
        }

        public int RefreshTokenExpiryInMinutes => _settings.RefreshExpiresInMinutes;
    }
}
