namespace EventBus.Shared.Events.Absracts
{
    public interface IOrderPaymentCompletedRequestEvent
    {
        public Guid OrderId { get; set; }
    }
}
