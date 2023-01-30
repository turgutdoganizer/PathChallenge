using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Core.Utilities.Extensions;
using Ordering.Core.Utilities.Results;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Pagination;
using Ordering.Service.Extensions.Pagination;
using System.Net;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public abstract class GenericController<T> : ControllerBase
    {
        protected readonly IMediator _mediator;

        protected GenericController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IPaginationResult<>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(Core.Utilities.Results.Abstract.IResult), (int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(ErrorResult), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Core.Utilities.Results.Abstract.IResult), (int)HttpStatusCode.Forbidden)]
        [ProducesResponseType(typeof(Core.Utilities.Results.Abstract.IResult), (int)HttpStatusCode.Unauthorized)]
        public virtual async Task<IActionResult> GetPaginationList([FromQuery] PaginationDto paginationDto)
        {
            var pagination = new PaginationBaseQuery<T>(paginationDto);
            var result = await _mediator.Send(pagination);
            return this.GetResult(result);
        }
    }
}
