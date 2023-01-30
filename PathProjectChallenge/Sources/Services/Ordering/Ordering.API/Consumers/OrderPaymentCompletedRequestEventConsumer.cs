using EventBus.Shared.Events.Abstracts;
using MassTransit;

namespace Ordering.API.Consumers
{
    public class OrderPaymentCompletedRequestEventConsumer : IConsumer<IOrderPaymentCompletedRequestEvent>
    {
        private readonly ILogger<IOrderPaymentCompletedRequestEvent> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IPublishEndpoint _publishEndpoint;
        //private readonly IMediator _mediator;

        public OrderPaymentCompletedRequestEventConsumer(ILogger<IOrderPaymentCompletedRequestEvent> logger, ISendEndpointProvider sendEndpointProvider, IPublishEndpoint publishEndpoint/*, IMediator mediator*/)
        {
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
            _publishEndpoint = publishEndpoint;
            //_mediator = mediator;
        }

        public async Task Consume(ConsumeContext<IOrderPaymentCompletedRequestEvent> context)
        {
            Console.WriteLine(context.Message.OrderId);
            Console.WriteLine("IOrderPaymentCompletedRequestEvent");
        }
    }
}
