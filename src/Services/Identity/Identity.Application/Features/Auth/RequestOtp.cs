using MediatR;
using BuildingBlocks.Common;

namespace Identity.Application.Commands
{
    public record RequestOtpCommand(string UserName) : MediatR.IRequest<BuildingBlocks.Common.Result>;

    public class RequestOtpHandler : MediatR.IRequestHandler<RequestOtpCommand, BuildingBlocks.Common.Result>
    {
        private readonly Contracts.IIdentityService _service;
        public RequestOtpHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<BuildingBlocks.Common.Result> Handle(RequestOtpCommand request, CancellationToken cancellationToken)
        {
            return await _service.RequestOtpAsync(request.UserName);
        }
    }
}
