using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Winecellar.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly ILogger<AuthController> _logger;
        public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(mediator) => _logger = logger;
    }
}
