using EventStore.ClientAPI;
using Ordering.Core.Entities;
using Ordering.Core.EventSourcing.Events.Orders;
using Ordering.Core.EventSourcing.Streams.Base;
using Ordering.Core.Repositories;
using Ordering.Service.Mappers;
using System.Text;
using System.Text.Json;

namespace Ordering.Event.WorkerService.Subscribers
{
    public class OrderWorkerService : BackgroundService
    {
        private readonly ILogger<OrderWorkerService> _logger;
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly IServiceProvider _serviceProvider;

        public OrderWorkerService(ILogger<OrderWorkerService> logger, IEventStoreConnection eventStoreConnection, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _eventStoreConnection = eventStoreConnection;
            _serviceProvider = serviceProvider;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(ScreamConfigurations.OrderStreamName, ScreamConfigurations.OrderGroupName, EventAppeared, autoAck: false);
        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2, int? arg3)
        {
            try
            {
                Console.WriteLine("Order Worker  - Event Appeared");
                var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, Ordering.Core");
                var eventData = Encoding.UTF8.GetString(arg2.Event.Data);
                var @event = JsonSerializer.Deserialize(eventData, type);
                using var scope = _serviceProvider.CreateScope();
                var orderAccessor = scope.ServiceProvider.GetRequiredService<Func<string, IOrderRepository>>();
                IOrderRepository orderSqlServerRepository = orderAccessor("SqlServer");
                IOrderRepository orderMongoRepository = orderAccessor("MongoDb");
                Order order;
                switch (@event)
                {
                    case OrderInsertedEvent orderInsertedEvent:
                        order = OrderMapper.Mapper.Map<Order>(orderInsertedEvent);
                        await orderSqlServerRepository.AddAsync(order);
                        await orderMongoRepository.AddAsync(order);
                        break;
                    case OrderUpdatedEvent orderUpdatedEvent:
                        order = OrderMapper.Mapper.Map<Order>(orderUpdatedEvent);
                        await orderSqlServerRepository.UpdateAsync(order);
                        await orderMongoRepository.UpdateAsync(order);
                        break;
                    case OrderDeletedEvent orderDeletedEvent:
                        order = OrderMapper.Mapper.Map<Order>(orderDeletedEvent);
                        await orderSqlServerRepository.DeleteAsync(order);
                        await orderMongoRepository.DeleteAsync(order);
                        break;
                }
                arg1.Acknowledge(arg2.Event.EventId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
