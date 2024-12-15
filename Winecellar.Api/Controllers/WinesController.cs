using MediatR;
using Microsoft.AspNetCore.Mvc;
using Winecellar.Application.Queries.Wines;
using Winecellar.Application.Commands.Wines;
using Winecellar.Api.Controllers;
using Winecellar.Application.Dtos.Wines;
using Microsoft.AspNetCore.Authorization;

namespace Winecellar.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class WineController : BaseController
    {
        private readonly ILogger<WineController> _logger;
        public WineController(ILogger<WineController> logger, IMediator mediator) : base(mediator) => _logger = logger;

        [HttpGet]
        public async Task<ActionResult<List<WineDto>>> GetWines()
        {
            return Ok(await _mediator.Send(new GetAllWinesQuery()));

        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateWine([FromBody] CreateWineRequestDto wine)
        {
            {
                return await _mediator.Send(new CreateWineCommand(wine));
            }
        }
    }
}
