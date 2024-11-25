using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Winecellar.Commands;
using Winecellar.Queries;

namespace Winecellar.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WinesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public WinesController(IMediator mediator) => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult> GetWines()
        {
            var wines = await _mediator.Send(new GetWinesQuery());

            return Ok(wines);
        }

        [HttpPost]
        public async Task<ActionResult> AddProduct([FromBody] Wine wine)
        {
            {
                await _mediator.Send(new AddWineCommand(wine));

                return StatusCode(201);
            }
        }
    }
}
