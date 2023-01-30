using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    public class AccountBalanceIsSufficientEvent : IAccountBalanceIsSufficientEvent
    {
        public Guid CorrelationId { get; set; }
      
        public AccountBalanceIsSufficientEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }
        public Guid AccountId { get; set; }
        public double TotalPrice { get; set; }
        public Guid CurrencyId { get; set; }


        public Guid UserId { get; set; }

       
    }
}
