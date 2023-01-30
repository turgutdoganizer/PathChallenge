using Catalog.Core.Domain;
using Catalog.Core.Domain.Catalog;
using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Events.Products;
using Catalog.Core.EventSourcing.Streams.Base;
using Catalog.Dto.Categories;
using EventStore.ClientAPI;
using PathProjectChallenge.Core.Infrastructure.Mapper;
using PathProjectChallenge.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Catalog.WorkerService.Subscribers
{
    internal class ProductWorkerService : BackgroundService
    {
        private readonly ILogger<ProductWorkerService> _logger;
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly IServiceProvider _serviceProvider;

        public ProductWorkerService(
           ILogger<ProductWorkerService> logger,
           IEventStoreConnection eventStoreConnection,
           IServiceProvider serviceProvider)
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
            Console.WriteLine("Hello Execute From Worker : category");
            try
            {
                await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(ScreamConfigurations.ProductStreamName, ScreamConfigurations.ProductGroupName, EventAppeared, autoAck: false);

            }
            catch (Exception ex)
            {
                Console.WriteLine("********************************************************");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                Console.WriteLine(ex.InnerException);
                Console.WriteLine(ex.Data.Values);
                Console.WriteLine("********************************************************");
            }
        }

        private async Task EventAppeared(EventStorePersistentSubscriptionBase arg1, ResolvedEvent arg2, int? arg3)
        {
            try
            {
                Console.WriteLine("category Worker  - Event Appeared");
                var type = Type.GetType($"{Encoding.UTF8.GetString(arg2.Event.Metadata)}, Catalog.Core");
                var eventData = Encoding.UTF8.GetString(arg2.Event.Data);
                var @event = JsonSerializer.Deserialize(eventData, type);
                using var scope = _serviceProvider.CreateScope();
                IRepository<Product> _productRepository = (IRepository<Product>)scope.ServiceProvider.GetRequiredService(typeof(IRepository<Product>));
                Product product;
                switch (@event)
                {
                    case ProductInsertedEvent productInsertedEvent:
                        var productInsertDto = AutoMapperConfiguration.Mapper.Map<CategoryInsertDto>(productInsertedEvent);
                        product = AutoMapperConfiguration.Mapper.Map<Product>(productInsertDto);
                        await _productRepository.InsertAsync(product);
                        //await categoryMongoRepository.AddAsync(category);
                        break;
                    case ProductUpdatedEvent productUpdatedEvent:
                        var productyUpdateDto = AutoMapperConfiguration.Mapper.Map<CategoryUpdatedEvent>(productUpdatedEvent);
                        product = AutoMapperConfiguration.Mapper.Map<Product>(productyUpdateDto);
                        await _productRepository.UpdateAsync(product);
                        //await categoryMongoRepository.UpdateAsync(category);
                        break;
                    case ProductDeletedEvent productDeletedEvent:
                        var productDeleteDto = AutoMapperConfiguration.Mapper.Map<CategoryDeletedEvent>(productDeletedEvent);
                        product = AutoMapperConfiguration.Mapper.Map<Product>(productDeleteDto);
                        await _productRepository.DeleteAsync(product);
                        //await categoryMongoRepository.DeleteAsync(category);
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
