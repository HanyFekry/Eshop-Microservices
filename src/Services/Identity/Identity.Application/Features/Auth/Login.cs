using MediatR;
using Identity.Application.Models;

namespace Identity.Application.Commands
{
    public record LoginCommand(string UserName, string Password) : MediatR.IRequest<BuildingBlocks.Common.Result<TokenResponseDto>>;

    public class LoginHandler : MediatR.IRequestHandler<LoginCommand, BuildingBlocks.Common.Result<TokenResponseDto>>
    {
        private readonly Contracts.IIdentityService _service;
        public LoginHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result<TokenResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var res = await _service.LoginAsync(request.UserName, request.Password);
            if (string.IsNullOrEmpty(res.AccessToken) && string.IsNullOrEmpty(res.RefreshToken))
                return BuildingBlocks.Common.Result<TokenResponseDto>.Failure(new[] { "Invalid credentials" });
            return BuildingBlocks.Common.Result<TokenResponseDto>.Success(res);
        }
    }
}
