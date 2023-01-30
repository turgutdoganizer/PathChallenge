namespace Catalog.Core.EventSourcing.Events.Products
{
    public class ProductDeletedEvent : IEvent
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    }
}
