using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class PaymentFailedEvent : IPaymentFailedEvent
    {
        public PaymentFailedEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid AccountId { get; set; }
        public string Reason { get; set; }

        public double Balance { get; set; }
        public Guid CorrelationId { get; }
    }
}
