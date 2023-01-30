using MediatR;
using Ordering.Core.Repositories;
using Ordering.Core.Utilities.Results;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Dto.Orders;
using Ordering.Service.Mappers;
using Ordering.Service.Services.Orders.Handlers.Base;
using Ordering.Service.Services.Orders.Queries;
using System.Net;

namespace Ordering.Service.Services.Orders.Handlers
{
    public class OrderListHandler : BaseOrderHandler, IRequestHandler<OrderListQuery, IResult>
    {
        public OrderListHandler(IOrderRepository orderRepository) : base(orderRepository)
        {
        }

        public async Task<IResult> Handle(OrderListQuery request, CancellationToken cancellationToken)
        {
            var orders = await _orderRepository.GetAllAsync();
            if (orders != null)
            {
                var mappedResult = OrderMapper.Mapper.Map<IEnumerable<OrderDto>>(orders);
                return new DataResult<IEnumerable<OrderDto>>(mappedResult, HttpStatusCode.OK, 1);
            }
            return new Result(HttpStatusCode.BadRequest, "Bad Request");
        }
    }
}
