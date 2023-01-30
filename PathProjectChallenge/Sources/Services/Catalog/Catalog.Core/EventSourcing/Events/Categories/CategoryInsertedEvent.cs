namespace Catalog.Core.EventSourcing.Events.Categories
{
    public class CategoryInsertedEvent : IEvent
    {
        //[NoMap]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
