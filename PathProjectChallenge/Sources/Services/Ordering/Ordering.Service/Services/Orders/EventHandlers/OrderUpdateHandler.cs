using MediatR;
using Ordering.Core.EventSourcing.Events.Orders;
using Ordering.Core.EventSourcing.Streams;
using Ordering.Core.Utilities.Results;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Service.Mappers;
using Ordering.Service.Services.Orders.Commands;
using System.Net;

namespace Ordering.Service.Services.Orders.EventHandlers
{
    public class OrderUpdateHandler : IRequestHandler<OrderUpdateCommand, IResult>
    {
        private readonly IOrderStream _orderStream;
        public OrderUpdateHandler(IOrderStream orderStream)
        {
            _orderStream = orderStream;
        }

        public async Task<IResult> Handle(OrderUpdateCommand request, CancellationToken cancellationToken)
        {
            var orderUpdatedEvent = OrderMapper.Mapper.Map<OrderUpdatedEvent>(request.OrderUpdateDto);
            _orderStream.Updated(orderUpdatedEvent);
            var result = await _orderStream.InsertAsync(Guid.NewGuid());
            return result ? new Result(HttpStatusCode.Created, "Order is updated successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
