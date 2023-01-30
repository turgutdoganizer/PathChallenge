using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Dto.Orders;
using Ordering.Service.Services.Orders.Commands;
using Ordering.Service.Services.Orders.Queries;

namespace Ordering.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : GenericController<OrderDto>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        public OrdersController(IMediator mediator, ISendEndpointProvider sendEndpointProvider) : base(mediator)
        {
            _sendEndpointProvider = sendEndpointProvider;
        }

        [HttpPost]
        public async Task<IActionResult> Insert(OrderInsertDto orderInsertDto)
        {
            return Ok(await _mediator.Send(new OrderInsertCommand() { OrderInsertDto = orderInsertDto }));
        }

        [HttpPut]
        public async Task<IActionResult> Update(OrderUpdateDto orderUpdateDto)
        {
            return Ok(await _mediator.Send(new OrderUpdateCommand() { OrderUpdateDto = orderUpdateDto }));
        }


        [HttpDelete]
        public async Task<IActionResult> Delete(OrderDeleteDto orderDeleteDto)
        {
            return Ok(await _mediator.Send(new OrderDeleteCommand() { OrderDeleteDto = orderDeleteDto }));
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _mediator.Send(new OrderListQuery()));
        }
    }
}
