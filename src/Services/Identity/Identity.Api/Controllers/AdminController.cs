using Identity.Application.Commands;
using BuildingBlocks.Common;
using Identity.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AdminController(IMediator mediator) => _mediator = mediator;

        [HttpGet("users")]
        public async Task<ActionResult<Result<IEnumerable<UserDto>>>> GetUsers()
        {
            var res = await _mediator.Send(new GetUsersQuery());
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("set-active")]
        public async Task<ActionResult<Result<bool>>> SetActive([FromBody] SetUserActiveCommand cmd)
        {
            var res = await _mediator.Send(cmd);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }
    }
}
