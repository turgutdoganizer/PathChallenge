namespace Catalog.Core.EventSourcing.Events.Categories
{
    public class CategoryDeletedEvent : IEvent
    {
        public Guid Id { get; set; }
    }
}
