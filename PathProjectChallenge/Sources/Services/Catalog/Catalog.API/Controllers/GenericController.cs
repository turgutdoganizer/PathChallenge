using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class GenericController : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected GenericController(IMediator mediator)
        {
            _mediator = mediator;
        }

    }
}
