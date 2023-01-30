using EventBus.Shared.Events.Abstracts;
using MassTransit;

namespace Ordering.API.Consumers
{
    public class OrderAccountBalanceIsInsufficientRequestEventConsumer : IConsumer<IOrderAccountBalanceIsInsufficientRequestEvent>
    {
        private readonly ILogger<IOrderAccountBalanceIsInsufficientRequestEvent> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderAccountBalanceIsInsufficientRequestEventConsumer(ILogger<IOrderAccountBalanceIsInsufficientRequestEvent> logger, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IOrderAccountBalanceIsInsufficientRequestEvent> context)
        {
            Console.WriteLine("IOrderAccountBalanceIsInsufficientRequestEvent");
            var orderId = context.Message.OrderId;
            var reason = context.Message.Reason;
            Console.WriteLine(orderId);
            Console.WriteLine(reason);
        }
    }
}
