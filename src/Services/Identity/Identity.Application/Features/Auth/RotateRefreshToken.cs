using MediatR;
using Identity.Application.Models;

namespace Identity.Application.Commands
{
    public record RotateRefreshTokenCommand(string RefreshToken) : MediatR.IRequest<BuildingBlocks.Common.Result<TokenResponseDto>>;

    public class RotateRefreshTokenHandler : MediatR.IRequestHandler<RotateRefreshTokenCommand, BuildingBlocks.Common.Result<TokenResponseDto>>
    {
        private readonly Contracts.IIdentityService _service;
        public RotateRefreshTokenHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result<TokenResponseDto>> Handle(RotateRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var res = await _service.RotateRefreshTokenAsync(request.RefreshToken);
            if (res == null) return BuildingBlocks.Common.Result<TokenResponseDto>.Failure(new[] { "Invalid or expired refresh token" });
            return BuildingBlocks.Common.Result<TokenResponseDto>.Success(res);
        }
    }
}
