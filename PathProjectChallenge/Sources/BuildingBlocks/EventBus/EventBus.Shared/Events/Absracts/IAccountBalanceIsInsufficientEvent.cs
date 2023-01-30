using MassTransit;

namespace EventBus.Shared.Events.Absracts
{
    public interface IAccountBalanceIsInsufficientEvent : CorrelatedBy<Guid>
    {
        public string Reason { get; set; }
    }
}
