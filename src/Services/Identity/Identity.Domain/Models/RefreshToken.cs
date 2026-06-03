namespace Identity.Domain.Models
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expires { get; set; }
        public DateTime Created { get; set; }
        public bool Revoked { get; set; }
        public string? ReplacedByToken { get; set; }
        public string? RemoteIpAddress { get; set; }

        // Navigation
        public string? ApplicationUserId { get; set; }

        // Helper properties
        public bool IsExpired => DateTime.UtcNow >= Expires;
        public bool IsActive => !Revoked && !IsExpired;

        // Mark token as revoked and optionally record replacement
        public void Revoke(string? replacedBy = null)
        {
            Revoked = true;
            ReplacedByToken = replacedBy;
        }
    }
}
