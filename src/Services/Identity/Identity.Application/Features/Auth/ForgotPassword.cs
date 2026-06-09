using MediatR;
using Identity.Application.Models;

namespace Identity.Application.Commands
{
    public record ForgotPasswordCommand(string UserName) : MediatR.IRequest<BuildingBlocks.Common.Result<ForgotPasswordResponseDto>>;

    public class ForgotPasswordHandler : MediatR.IRequestHandler<ForgotPasswordCommand, BuildingBlocks.Common.Result<ForgotPasswordResponseDto>>
    {
        private readonly Contracts.IIdentityService _service;
        public ForgotPasswordHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result<ForgotPasswordResponseDto>> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var res = await _service.ForgotPasswordAsync(request.UserName);
            if (!res.Succeeded) return BuildingBlocks.Common.Result<ForgotPasswordResponseDto>.Failure(new[] { "User not found" });
            return BuildingBlocks.Common.Result<ForgotPasswordResponseDto>.Success(res);
        }
    }
}
