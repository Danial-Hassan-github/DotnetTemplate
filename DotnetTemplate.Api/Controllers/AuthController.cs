using Microsoft.AspNetCore.Mvc;
using MediatR;
using DotnetTemplate.Application.Features.Auth.Commands;
using DotnetTemplate.Application.DTOs;

namespace DotnetTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IMediator mediator) : ControllerBase
    {
       [HttpPost("Register")]
       public async Task<IActionResult> RegisterUserAsync([FromBody] RegisterRequest registerRequest)
        {
            var result = await mediator.Send(new RegisterCommand(registerRequest));
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginRequest loginRequest)
        {
            var result = await mediator.Send(new LoginCommand(loginRequest));
            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(string refreshToken)
        {
            var result = await mediator.Send(new RefreshTokenCommand(refreshToken));
            return Ok(result);
        }
    }
}