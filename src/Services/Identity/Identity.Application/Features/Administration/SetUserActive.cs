using MediatR;

namespace Identity.Application.Commands
{
    public record SetUserActiveCommand(string UserName, bool IsActive) : MediatR.IRequest<Result<bool>>;

    public class SetUserActiveHandler : MediatR.IRequestHandler<SetUserActiveCommand, Result<bool>>
    {
        private readonly Contracts.IIdentityService _service;
        public SetUserActiveHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<Result<bool>> Handle(SetUserActiveCommand request, CancellationToken cancellationToken)
        {
            var res = await _service.SetUserActiveAsync(request.UserName, request.IsActive);
            if (!res) return Result<bool>.Failure(new[] { "User not found" });
            return Result<bool>.Success(true);
        }
    }
}
