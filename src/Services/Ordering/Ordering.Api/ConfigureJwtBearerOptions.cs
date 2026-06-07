using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Ordering.Api
{
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly JwtSettings _settings;

        public ConfigureJwtBearerOptions(IOptions<JwtSettings> settings)
        {
            _settings = settings?.Value ?? throw new ArgumentNullException(nameof(settings));
        }

        public void Configure(string? name, JwtBearerOptions options) => Configure(options);

        public void Configure(JwtBearerOptions options)
        {
            if (string.IsNullOrEmpty(_settings.Key))
            {
                throw new ArgumentNullException(nameof(_settings.Key), "JWT signing key is not configured.");
            }

            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Key!)),
                ValidateIssuer = !string.IsNullOrEmpty(_settings.Issuer),
                ValidIssuer = _settings.Issuer,
                ValidateAudience = !string.IsNullOrEmpty(_settings.Audience),
                ValidAudience = _settings.Audience,
                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                // Enforce configured token lifetime when issuing tokens by checking iat and exp
                LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
                {
                    if (expires == null)
                        return false;

                    // token expired
                    if (expires <= DateTime.UtcNow)
                        return false;

                    // If no limit configured, accept valid non-expired tokens
                    if (_settings.ExpiresInMinutes <= 0)
                        return true;

                    // Try to read issued-at (iat) from token and ensure it doesn't claim a longer lifetime
                    if (securityToken is JwtSecurityToken jwt)
                    {
                        if (jwt.Payload.TryGetValue("iat", out var iatObj) && iatObj != null)
                        {
                            long iatSeconds = 0;
                            try
                            {
                                switch (iatObj)
                                {
                                    case long l: iatSeconds = l; break;
                                    case int i: iatSeconds = i; break;
                                    case double d: iatSeconds = Convert.ToInt64(d); break;
                                    case string s when long.TryParse(s, out var tmp): iatSeconds = tmp; break;
                                }

                                if (iatSeconds > 0)
                                {
                                    var issued = DateTimeOffset.FromUnixTimeSeconds(iatSeconds).UtcDateTime;
                                    var maxExp = issued.AddMinutes(_settings.ExpiresInMinutes);
                                    // if token's exp is after allowed maximum, reject
                                    if (expires > maxExp)
                                        return false;
                                }
                            }
                            catch
                            {
                                // if any parsing error, fall back to default lifetime validation
                            }
                        }
                    }

                    return true;
                }
            };
        }
    }
}
