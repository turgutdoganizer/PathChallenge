using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class AccountBalanceIsSufficientPaymentRequestEvent : IAccountBalanceIsSufficientPaymentRequestEvent
    {
        public AccountBalanceIsSufficientPaymentRequestEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public Guid AccountId { get; set; }
        public double TotalPrice { get; set; }

        public Guid CurrencyId { get; set; }

        public Guid UserId { get; set; }
        public Guid CorrelationId { get; }
    }
}
