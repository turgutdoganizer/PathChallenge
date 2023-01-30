using Ordering.Core.Entities;

namespace Ordering.Core.EventSourcing.Events.Orders
{
    public class OrderUpdatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPrice { get; set; }
        public OrderStatus Status { get; set; }
    }
}
