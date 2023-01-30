using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class OrderPaymentFailedRequestEvent : IOrderPaymentFailedRequestEvent
    {
        public Guid OrderId { get; set; }
        public string Reason { get; set; }
    }
}
