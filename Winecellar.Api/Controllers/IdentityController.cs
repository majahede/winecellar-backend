using MediatR;
using Microsoft.AspNetCore.Mvc;
using Winecellar.Application.Commands.Identity;
using Winecellar.Application.Dtos.Identity;
using Winecellar.Application.Dtos.Token;

namespace Winecellar.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class IdentityController : BaseController
    {
        public IdentityController(IMediator mediator) : base(mediator) { }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> RegisterAccount([FromBody] RegisterUserRequestDto registerUserRequestDto)
        {
            await _mediator.Send( new RegisterUserCommand(registerUserRequestDto));

            return Ok("Successfully registered");
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDto>> Login(LoginUserRequestDto loginUserRequestDto)
        {
            return Ok( await _mediator.Send(new LoginUserCommand(loginUserRequestDto)));
        }
    }
}
