using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Winecellar.Api.Controllers
{

    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        internal readonly IMediator _mediator;

        protected BaseController(IMediator mediator) => _mediator = mediator;

        protected string CurrentToken
        {
            get
            {
                string authorization = Request.Headers["Authorization"];

                return authorization.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
                    ? authorization["Bearer ".Length..].Trim()
                    : string.Empty;
            }
        }

    }
}
