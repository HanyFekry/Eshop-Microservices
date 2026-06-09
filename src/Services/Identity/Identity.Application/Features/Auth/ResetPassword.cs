using MediatR;
using Identity.Application.Models;

namespace Identity.Application.Commands
{
    public record ResetPasswordCommand(string UserName, string Token, string NewPassword) : MediatR.IRequest<BuildingBlocks.Common.Result<ResetPasswordResponseDto>>;

    public class ResetPasswordHandler : MediatR.IRequestHandler<ResetPasswordCommand, BuildingBlocks.Common.Result<ResetPasswordResponseDto>>
    {
        private readonly Contracts.IIdentityService _service;
        public ResetPasswordHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result<ResetPasswordResponseDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await _service.ResetPasswordAsync(request.UserName, request.Token, request.NewPassword);
            if (!res.Succeeded) return BuildingBlocks.Common.Result<ResetPasswordResponseDto>.Failure(res.Errors ?? Enumerable.Empty<string>());
            return BuildingBlocks.Common.Result<ResetPasswordResponseDto>.Success(res);
        }
    }
}
