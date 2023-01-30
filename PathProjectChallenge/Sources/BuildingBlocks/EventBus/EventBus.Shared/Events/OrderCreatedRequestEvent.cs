using EventBus.Shared.Events.Absracts;
using EventBus.Shared.Messages;

namespace EventBus.Shared.Events
{
    public class OrderCreatedRequestEvent : IOrderCreatedRequestEvent
    {
        public Guid OrderId { get; set; }
        public Guid UserId { get; set; }
        //public List<OrderItemMessage> OrderItems { get; set; } = new List<OrderItemMessage>();
        public Guid AccountId { get; set; }
        public Guid CurrencyId { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalAmount { get; set; }
        public double TotalPrice { get; set; }
    }
}
