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
    public class OrderDeleteHandler : IRequestHandler<OrderDeleteCommand, IResult>
    {
        private readonly IOrderStream _orderStream;
        public OrderDeleteHandler(IOrderStream orderStream)
        {
            _orderStream = orderStream;
        }

        public async Task<IResult> Handle(OrderDeleteCommand request, CancellationToken cancellationToken)
        {
            var orderDeletedEvent = OrderMapper.Mapper.Map<OrderDeletedEvent>(request.OrderDeleteDto);
            _orderStream.Deleted(orderDeletedEvent);
            var result = await _orderStream.InsertAsync(Guid.NewGuid());
            return result ? new Result(HttpStatusCode.Created, "Order is deleted successfully") : new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
