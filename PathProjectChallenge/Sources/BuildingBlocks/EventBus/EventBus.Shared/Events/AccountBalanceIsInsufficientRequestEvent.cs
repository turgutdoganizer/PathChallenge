using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class OrderAccountBalanceIsInsufficientRequestEvent : IOrderAccountBalanceIsInsufficientRequestEvent
    {
        public Guid OrderId { get; set; }
        public string Reason { get; set; }
    }
}
