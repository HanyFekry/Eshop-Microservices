using Identity.Application.Commands;
using BuildingBlocks.Common;
using Identity.Application.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator) => _mediator = mediator;

        [HttpPost("register")]
        public async Task<ActionResult<Result<RegisterResponseDto>>> Register([FromBody] RegisterCommand command)
        {
            var res = await _mediator.Send(command);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("login")]
        public async Task<ActionResult<Result<TokenResponseDto>>> Login([FromBody] LoginCommand command)
        {
            var res = await _mediator.Send(command);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<Result<ForgotPasswordResponseDto>>> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            var res = await _mediator.Send(command);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<Result<ResetPasswordResponseDto>>> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            var res = await _mediator.Send(command);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }

        [HttpPost("rotate-refresh")]
        public async Task<ActionResult<Result<TokenResponseDto>>> RotateRefresh([FromBody] RotateRefreshTokenCommand command)
        {
            var res = await _mediator.Send(command);
            if (!res.Succeeded) return BadRequest(res);
            return Ok(res);
        }
    }
}
