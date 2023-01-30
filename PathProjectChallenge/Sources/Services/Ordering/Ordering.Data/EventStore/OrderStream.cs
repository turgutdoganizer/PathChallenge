using EventStore.ClientAPI;
using Ordering.Core.EventSourcing.Events.Orders;
using Ordering.Core.EventSourcing.Streams;
using Ordering.Core.EventSourcing.Streams.Base;

namespace Ordering.Data.EventStore
{
    public class OrderStream : AbstractStream, IOrderStream
    {
        public static string StreamName = ScreamConfigurations.OrderStreamName;

        public OrderStream(IEventStoreConnection eventStoreConnection) : base(StreamName, eventStoreConnection)
        {
        }
        public void Inserted(OrderInsertedEvent orderInsertedEvent)
        {
            Events.AddLast(orderInsertedEvent);
        }
        public void Updated(OrderUpdatedEvent orderUpdatedEvent)
        {
            Events.AddLast(orderUpdatedEvent);
        }

        public void Deleted(OrderDeletedEvent orderDeletedEvent)
        {
            Events.AddLast(orderDeletedEvent);
        }
    }
}
