namespace Identity.Application.Contracts
{
    public interface IJwtService
    {
        string CreateAccessToken(Identity.Domain.ApplicationUser user, IEnumerable<string>? roles = null);
        string GenerateRefreshToken();
        int RefreshTokenExpiryInMinutes { get; }
    }
}
