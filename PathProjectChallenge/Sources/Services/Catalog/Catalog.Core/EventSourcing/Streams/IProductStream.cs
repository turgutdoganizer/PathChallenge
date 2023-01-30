using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Core.EventSourcing.Streams.Base;

namespace Catalog.Core.EventSourcing.Streams
{
    public interface IProductStream : IStream
    {
        public void Inserted(ProductInsertedEvent productInsertedEvent);

        public void Updated(ProductUpdatedEvent productUpdatedEvent);

        public void Deleted(ProductDeletedEvent productDeletedEvent);
    }
}
