using MediatR;
using Identity.Application.Models;

namespace Identity.Application.Commands
{
    public record RegisterCommand(string UserName, string Password, string? Email) : MediatR.IRequest<BuildingBlocks.Common.Result<RegisterResponseDto>>;

    public class RegisterHandler : MediatR.IRequestHandler<RegisterCommand, BuildingBlocks.Common.Result<RegisterResponseDto>>
    {
        private readonly Contracts.IIdentityService _service;
        public RegisterHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result<RegisterResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            var res = await _service.RegisterAsync(request.UserName, request.Password, request.Email);
            if (!res.Succeeded) return BuildingBlocks.Common.Result<RegisterResponseDto>.Failure(res.Errors ?? Enumerable.Empty<string>());
            return BuildingBlocks.Common.Result<RegisterResponseDto>.Success(res);
        }
    }
}
