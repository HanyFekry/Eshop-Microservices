using Identity.Domain.Models;
using BuildingBlocks.Common;

namespace Identity.Application.Contracts
{
    public interface IIdentityService
    {
        Task<Identity.Application.Models.RegisterResponseDto> RegisterAsync(string userName, string password, string? email);
        Task<Identity.Application.Models.TokenResponseDto> LoginAsync(string userName, string password);
        Task<BuildingBlocks.Common.Result<Identity.Application.Models.TokenResponseDto>> LoginWithOtpAsync(string userName, string otp);
        Task<Identity.Application.Models.ForgotPasswordResponseDto> ForgotPasswordAsync(string userName);
        Task<Identity.Application.Models.ResetPasswordResponseDto> ResetPasswordAsync(string userName, string token, string newPassword);
        Task<IEnumerable<Identity.Domain.Models.UserDto>> GetAllUsersAsync();
        Task<bool> SetUserActiveAsync(string userName, bool isActive);
        Task<Identity.Application.Models.TokenResponseDto?> RotateRefreshTokenAsync(string refreshToken);
        Task<Result> RequestOtpAsync(string userName);
    }
}
