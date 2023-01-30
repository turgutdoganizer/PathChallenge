using Catalog.Core.EventSourcing.Events;
using EventStore.ClientAPI;
using System.Text;
using System.Text.Json;

namespace Catalog.Core.EventSourcing.Streams.Base
{
    public abstract class AbstractStream
    {
        protected readonly LinkedList<IEvent> Events = new LinkedList<IEvent>();

        private string _streamName { get; }

        private readonly IEventStoreConnection _eventStoreConnection;

        protected AbstractStream(string streamName, IEventStoreConnection eventStoreConnection)
        {
            _streamName = streamName;
            _eventStoreConnection = eventStoreConnection;
        }

        public async Task<bool> InsertAsync(Guid id)
        {
            var newEvents = Events.ToList().Select(x => new EventData(
                 id,
                 x.GetType().Name,
                 true,
                 Encoding.UTF8.GetBytes(JsonSerializer.Serialize(x, inputType: x.GetType())),
                 Encoding.UTF8.GetBytes(x.GetType().FullName))).ToList();

            WriteResult result = await _eventStoreConnection.AppendToStreamAsync(_streamName, ExpectedVersion.Any, newEvents);
            Events.Clear();
            if (result.NextExpectedVersion > 0)
                return true;
            return false;
        }
    }
}
