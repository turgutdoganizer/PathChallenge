using Ordering.Core.EventSourcing.Events.Orders;
using Ordering.Core.EventSourcing.Streams.Base;

namespace Ordering.Core.EventSourcing.Streams
{
    public interface IOrderStream : IStream
    {
        public void Inserted(OrderInsertedEvent orderInsertedEvent);

        public void Updated(OrderUpdatedEvent orderUpdatedEvent);

        public void Deleted(OrderDeletedEvent orderDeletedEvent);
    }
}
