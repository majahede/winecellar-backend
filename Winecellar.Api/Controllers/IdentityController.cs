using MediatR;
using Microsoft.AspNetCore.Mvc;
using Winecellar.Application.Commands.Identity;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        internal readonly IMediator _mediator;
        public IdentityController(IMediator mediator) => _mediator = mediator;

        [HttpPost]
        [Route("register")] //api/v1/identity/register
        public async Task<ActionResult> RegisterAccount([FromBody] RegisterUserRequestDto registerUserRequestDto)
        {
            await _mediator.Send( new RegisterUserCommand(registerUserRequestDto));

            return Ok("Successfully registered");
        }

        [HttpPost]
        [Route("login")] //api/v1/identity/login
        public async Task<ActionResult<TokenDto>> Login(LoginUserRequestDto loginUserRequestDto)
        {
            return Ok( await _mediator.Send(new LoginUserCommand(loginUserRequestDto)));
        }
    }
}
