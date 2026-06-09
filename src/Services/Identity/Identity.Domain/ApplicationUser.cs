using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Identity.Domain
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsActive { get; set; } = true;

        public ICollection<Identity.Domain.Models.RefreshToken> RefreshTokens { get; set; } = new List<Identity.Domain.Models.RefreshToken>();
    }
}
