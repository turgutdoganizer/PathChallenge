using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Core.EventSourcing.Streams.Base;
using EventStore.ClientAPI;

namespace Catalog.Data.EventSourcing.Streams
{
    public class CategoryStream : AbstractStream, ICategoryStream
    {
        public static string StreamName = ScreamConfigurations.CategoryStreamName;

        public CategoryStream(IEventStoreConnection eventStoreConnection) : base(StreamName, eventStoreConnection)
        {
        }
        public void Inserted(CategoryInsertedEvent CategoryInsertedEvent)
        {
            Events.AddLast(CategoryInsertedEvent);
        }
        public void Updated(CategoryUpdatedEvent CategoryUpdatedEvent)
        {
            Events.AddLast(CategoryUpdatedEvent);
        }

        public void Deleted(CategoryDeletedEvent CategoryDeletedEvent)
        {
            Events.AddLast(CategoryDeletedEvent);
        }
    }
}
