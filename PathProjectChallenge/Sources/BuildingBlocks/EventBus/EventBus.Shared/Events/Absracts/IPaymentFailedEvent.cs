using MassTransit;

namespace EventBus.Shared.Events.Absracts
{
    public interface IPaymentFailedEvent : CorrelatedBy<Guid>
    {
        public string Reason { get; set; }
        public Guid AccountId { get; set; }
        public double Balance { get; set; }
    }
}
