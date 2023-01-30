namespace Catalog.Core.EventSourcing.Events.Products
{
    public class ProductUpdatedEvent : IEvent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public bool UnitsInStock { get; set; }
        public int Quantity { get; set; }
        public string PictureFileName { get; set; }
        public string PictureUri { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedDate { get; set; } = DateTime.UtcNow;
    }
}
