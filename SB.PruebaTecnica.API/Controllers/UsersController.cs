using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SB.PruebaTecnica.Application.Commands.Users;

namespace SB.PruebaTecnica.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] CreateUserCommand command)
        {
            var user = await _mediator.Send(command);
            if (user == null)
            {
                return BadRequest("User could not be created.");
            }

            return Ok(user);
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserCommand command)
        {
            var loginResponse = await _mediator.Send(command);
            if (loginResponse == null)
            {
                return Unauthorized("Invalid credentials.");
            }

            return Ok(loginResponse);
        }
    }
}
