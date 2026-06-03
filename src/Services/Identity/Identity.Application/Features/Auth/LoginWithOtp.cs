using MediatR;
using BuildingBlocks.Common;
using Identity.Application.Models;

namespace Identity.Application.Commands
{
    public record LoginWithOtpCommand(string UserName, string Otp) : MediatR.IRequest<BuildingBlocks.Common.Result<TokenResponseDto>>;

    public class LoginWithOtpHandler : MediatR.IRequestHandler<LoginWithOtpCommand, BuildingBlocks.Common.Result<TokenResponseDto>>
    {
        private readonly Contracts.IIdentityService _service;
        public LoginWithOtpHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result<TokenResponseDto>> Handle(LoginWithOtpCommand request, CancellationToken cancellationToken)
        {
            return await _service.LoginWithOtpAsync(request.UserName, request.Otp);
        }
    }
}
