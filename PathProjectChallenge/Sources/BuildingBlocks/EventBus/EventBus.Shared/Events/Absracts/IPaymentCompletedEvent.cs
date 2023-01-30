using MassTransit;

namespace EventBus.Shared.Events.Absracts
{
    public interface IPaymentCompletedEvent : CorrelatedBy<Guid>
    {
    }
}
