using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Identity.Domain;
using Identity.Domain.Models;

namespace Identity.Infrastructure
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Identity.Domain.Models.RefreshToken> RefreshTokens { get; set; } = null!;
        public DbSet<Identity.Domain.Otp> Otps { get; set; } = null!;
    }
}
