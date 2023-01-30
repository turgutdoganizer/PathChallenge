using EventBus.Shared.Events.Absracts;

namespace EventBus.Shared.Events
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountBalanceIsInsufficientEvent : IAccountBalanceIsInsufficientEvent
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="correlationId"></param>
        public AccountBalanceIsInsufficientEvent(Guid correlationId)
        {
            CorrelationId = correlationId;
        }

        public string Reason { get; set; }

        public Guid CorrelationId { get; }
    }
}
