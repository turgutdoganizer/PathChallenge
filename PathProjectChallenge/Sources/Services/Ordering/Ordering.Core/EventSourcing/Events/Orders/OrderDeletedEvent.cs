namespace Ordering.Core.EventSourcing.Events.Orders
{
    public class OrderDeletedEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
