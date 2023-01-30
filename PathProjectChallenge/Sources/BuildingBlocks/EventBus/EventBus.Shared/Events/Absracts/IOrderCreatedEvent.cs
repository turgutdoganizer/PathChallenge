using MassTransit;

namespace EventBus.Shared.Events.Absracts
{
    public interface IOrderCreatedEvent : CorrelatedBy<Guid>
    {
        public Guid AccountId { get; set; }
        public Guid UserId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalPrice { get; set; }
    }
}
