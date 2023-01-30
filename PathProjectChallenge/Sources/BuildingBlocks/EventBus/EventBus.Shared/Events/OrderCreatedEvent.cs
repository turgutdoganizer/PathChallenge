using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class OrderCreatedEvent : IOrderCreatedEvent
    {
        public Guid CorrelationId { get; }

        public OrderCreatedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalPrice { get; set; }
    }
}
