using EventBus.Shared.Events.Abstracts;
using MassTransit;

namespace Ordering.API.Consumers
{
    public class OrderPaymentFailedRequestEventConsumer : IConsumer<IOrderPaymentFailedRequestEvent>
    {
        private readonly ILogger<IOrderAccountBalanceIsInsufficientRequestEvent> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;

        public OrderPaymentFailedRequestEventConsumer(ILogger<IOrderAccountBalanceIsInsufficientRequestEvent> logger, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
        }

        public async Task Consume(ConsumeContext<IOrderPaymentFailedRequestEvent> context)
        {
            Console.WriteLine("OrderPaymentFailedRequestEventConsumer");
            Console.WriteLine(context.Message.OrderId);
            Console.WriteLine(context.Message.Reason);
        }
    }
}
