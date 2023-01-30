using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Streams.Base;

namespace Catalog.Core.EventSourcing.Streams
{
    public interface ICategoryStream : IStream
    {
        public void Inserted(CategoryInsertedEvent categoryInsertedEvent);

        public void Updated(CategoryUpdatedEvent categoryUpdatedEvent);

        public void Deleted(CategoryDeletedEvent categoryDeletedEvent);
    }
}
