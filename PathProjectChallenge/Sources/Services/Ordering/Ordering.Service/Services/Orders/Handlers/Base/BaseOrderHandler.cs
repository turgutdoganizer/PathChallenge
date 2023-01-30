using Ordering.Core.Repositories;

namespace Ordering.Service.Services.Orders.Handlers.Base
{
    public class BaseOrderHandler
    {
        protected readonly IOrderRepository _orderRepository;
        public BaseOrderHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
    }
}
