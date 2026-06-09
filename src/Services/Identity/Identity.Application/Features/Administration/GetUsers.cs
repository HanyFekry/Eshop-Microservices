using Identity.Domain.Models;

namespace Identity.Application.Commands
{
    public record GetUsersQuery() : MediatR.IRequest<Result<IEnumerable<UserDto>>>;

    public class GetUsersHandler : MediatR.IRequestHandler<GetUsersQuery, Result<IEnumerable<UserDto>>>
    {
        private readonly Contracts.IIdentityService _service;
        public GetUsersHandler(Contracts.IIdentityService service) => _service = service;

        public async Task<Result<IEnumerable<UserDto>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await _service.GetAllUsersAsync();
            return Result<IEnumerable<UserDto>>.Success(users);
        }
    }
}
