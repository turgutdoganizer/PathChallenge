namespace EventBus.Shared.Events.Absracts
{
    public interface IOrderPaymentFailedRequestEvent
    {
        public Guid OrderId { get; set; }

        public string Reason { get; set; }
    }
}
