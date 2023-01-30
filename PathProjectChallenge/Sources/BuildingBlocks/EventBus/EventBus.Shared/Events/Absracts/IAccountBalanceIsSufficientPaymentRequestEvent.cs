using MassTransit;

namespace EventBus.Shared.Events.Absracts
{
    public interface IAccountBalanceIsSufficientPaymentRequestEvent : CorrelatedBy<Guid>
    {
        public Guid AccountId { get; set; }
        public double TotalPrice { get; set; }
        public Guid UserId { get; set; }

        public Guid CurrencyId  { get; set; }
    }
}
