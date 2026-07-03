using Microsoft.AspNetCore.Mvc;
using MediatR;
using DotnetTemplate.Application.Features.Users.Commands;
using DotnetTemplate.Application.DTOs;

namespace DotnetTemplate.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
       [HttpPost("Register")]
       public async Task<IActionResult> RegisterUserAsync([FromBody] UserRegisterRequestDto registerRequest)
        {
            var result = await mediator.Send(new UserRegisterCommand(registerRequest));
            return Ok(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] UserLoginRequestDto loginRequest)
        {
            var result = await mediator.Send(new UserLoginCommand(loginRequest));
            return Ok(result);
        }
    }
}