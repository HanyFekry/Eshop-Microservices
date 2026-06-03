using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Identity.Application.Contracts;
using Identity.Application.Models;
using Identity.Domain;
using Identity.Domain.Models;
using BuildingBlocks.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly Identity.Application.Contracts.IJwtService _jwtService;
        private readonly ApplicationDbContext _db;

        public IdentityService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, Identity.Application.Contracts.IJwtService jwtService, ApplicationDbContext db)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _jwtService = jwtService ?? throw new ArgumentNullException(nameof(jwtService));
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<RegisterResponseDto> RegisterAsync(string userName, string password, string? email)
        {
            var user = new ApplicationUser { UserName = userName, Email = email };
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
                return new RegisterResponseDto(false, result.Errors.Select(e => e.Description), null);

            var defaultRole = "User";
            if (!await _roleManager.RoleExistsAsync(defaultRole))
                await _roleManager.CreateAsync(new IdentityRole(defaultRole));

            await _userManager.AddToRoleAsync(user, defaultRole);
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return new RegisterResponseDto(true, null, token);
        }

        public async Task<TokenResponseDto> LoginAsync(string userName, string password)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new TokenResponseDto(string.Empty, string.Empty);
            if (!await _userManager.CheckPasswordAsync(user, password)) return new TokenResponseDto(string.Empty, string.Empty);

            var roles = await _userManager.GetRolesAsync(user);
            var access = _jwtService.CreateAccessToken(user, roles);
            var refresh = _jwtService.GenerateRefreshToken();

            var rt = new Identity.Domain.Models.RefreshToken
            {
                Token = refresh,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_jwtService.RefreshTokenExpiryInMinutes),
                RemoteIpAddress = null,
                Revoked = false,
                ApplicationUserId = user.Id
            };

            user.RefreshTokens.Add(rt);
            await _userManager.UpdateAsync(user);
            await _db.SaveChangesAsync();

            return new TokenResponseDto(access, refresh);
        }

        public async Task<ForgotPasswordResponseDto> ForgotPasswordAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new ForgotPasswordResponseDto(false, null);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            return new ForgotPasswordResponseDto(true, token);
        }

        public async Task<ResetPasswordResponseDto> ResetPasswordAsync(string userName, string token, string newPassword)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return new ResetPasswordResponseDto(false, new[] { "User not found" });
            var result = await _userManager.ResetPasswordAsync(user, token, newPassword);
            if (!result.Succeeded) return new ResetPasswordResponseDto(false, result.Errors.Select(e => e.Description));
            return new ResetPasswordResponseDto(true, null);
        }

        public async Task<IEnumerable<Identity.Domain.Models.UserDto>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var list = new List<Identity.Domain.Models.UserDto>();
            foreach (var u in users)
            {
                var roles = await _userManager.GetRolesAsync(u);
                list.Add(new Identity.Domain.Models.UserDto(u.UserName ?? string.Empty, u.Email, roles.ToArray()));
            }
            return list;
        }

        public async Task<bool> SetUserActiveAsync(string userName, bool isActive)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return false;
            user.IsActive = isActive;
            await _userManager.UpdateAsync(user);
            return true;
        }

        public async Task<TokenResponseDto?> RotateRefreshTokenAsync(string refreshToken)
        {
            var rt = await _db.RefreshTokens.FirstOrDefaultAsync(t => t.Token == refreshToken);
            if (rt == null || rt.Revoked || rt.Expires <= DateTime.UtcNow) return null;

            // revoke current
            rt.Revoked = true;
            var user = await _userManager.FindByIdAsync(rt.ApplicationUserId);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var access = _jwtService.CreateAccessToken(user, roles);
            var newRefresh = _jwtService.GenerateRefreshToken();

            var newRt = new Identity.Domain.Models.RefreshToken
            {
                Token = newRefresh,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_jwtService.RefreshTokenExpiryInMinutes),
                Revoked = false,
                ApplicationUserId = user.Id
            };

            rt.ReplacedByToken = newRefresh;
            user.RefreshTokens.Add(newRt);
            await _userManager.UpdateAsync(user);
            await _db.SaveChangesAsync();

            return new TokenResponseDto(access, newRefresh);
        }

        public async Task<Result> RequestOtpAsync(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return Result.Failure(new[] { "User not found" }, 404);

            var code = new Random().Next(100000, 999999).ToString();
            var otp = new Identity.Domain.Otp
            {
                Code = code,
                Expires = DateTime.UtcNow.AddMinutes(5),
                Created = DateTime.UtcNow,
                Attempts = 0,
                IsUsed = false,
                ApplicationUserId = user.Id
            };

            await _db.AddAsync(otp);
            await _db.SaveChangesAsync();

            // TODO: send OTP to user via email/SMS
            return Result.Success(message: "OTP generated");
        }

        public async Task<BuildingBlocks.Common.Result<TokenResponseDto>> LoginWithOtpAsync(string userName, string otpCode)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user == null) return BuildingBlocks.Common.Result<TokenResponseDto>.Failure(new[] { "User not found" }, 404);

            var otp = await _db.Set<Identity.Domain.Otp>()
                .Where(o => o.ApplicationUserId == user.Id && !o.IsUsed && o.Expires > DateTime.UtcNow)
                .OrderByDescending(o => o.Id)
                .FirstOrDefaultAsync();

            if (otp == null) return BuildingBlocks.Common.Result<TokenResponseDto>.Failure(new[] { "No active OTP found" }, 401);

            // validate code and register attempts
            if (!otp.IsValidCode(otpCode))
            {
                otp.RegisterAttempt(false);
                await _db.SaveChangesAsync();
                return BuildingBlocks.Common.Result<TokenResponseDto>.Failure(new[] { "Invalid OTP" }, 401);
            }

            // mark used
            otp.RegisterAttempt(true);
            await _db.SaveChangesAsync();

            var roles = await _userManager.GetRolesAsync(user);
            var access = _jwtService.CreateAccessToken(user, roles);
            var refresh = _jwtService.GenerateRefreshToken();

            var rt = new Identity.Domain.Models.RefreshToken
            {
                Token = refresh,
                Created = DateTime.UtcNow,
                Expires = DateTime.UtcNow.AddMinutes(_jwtService.RefreshTokenExpiryInMinutes),
                RemoteIpAddress = null,
                Revoked = false,
                ApplicationUserId = user.Id
            };

            user.RefreshTokens.Add(rt);
            await _userManager.UpdateAsync(user);
            await _db.SaveChangesAsync();

            return BuildingBlocks.Common.Result<TokenResponseDto>.Success(new TokenResponseDto(access, refresh));
        }
    }
}
