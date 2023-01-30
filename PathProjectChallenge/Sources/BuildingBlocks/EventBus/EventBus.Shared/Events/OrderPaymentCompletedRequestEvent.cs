using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class OrderPaymentCompletedRequestEvent : IOrderPaymentCompletedRequestEvent
    {
        public Guid OrderId { get; set; }
    }
}
