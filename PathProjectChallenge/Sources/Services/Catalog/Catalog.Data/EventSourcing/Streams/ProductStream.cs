using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Core.EventSourcing.Streams.Base;
using EventStore.ClientAPI;

namespace Catalog.Data.EventSourcing.Streams
{
    public class ProductStream : AbstractStream, IProductStream
    {
        public static string StreamName = ScreamConfigurations.ProductStreamName;

        public ProductStream(IEventStoreConnection eventStoreConnection) : base(StreamName, eventStoreConnection)
        {
        }
        public void Inserted(ProductInsertedEvent ProductInsertedEvent)
        {
            Events.AddLast(ProductInsertedEvent);
        }
        public void Updated(ProductUpdatedEvent ProductUpdatedEvent)
        {
            Events.AddLast(ProductUpdatedEvent);
        }

        public void Deleted(ProductDeletedEvent ProductDeletedEvent)
        {
            Events.AddLast(ProductDeletedEvent);
        }
    }
}
