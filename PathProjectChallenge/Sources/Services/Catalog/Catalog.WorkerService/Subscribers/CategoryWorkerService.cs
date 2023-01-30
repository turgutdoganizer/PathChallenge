using Catalog.Core.Domain.Catalog;
using Catalog.Core.EventSourcing.Events.Categories;
using Catalog.Core.EventSourcing.Streams.Base;
using Catalog.Dto.Categories;
using Catalog.Service.Services.Categories.Commands;
using EventStore.ClientAPI;
using MediatR;
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
    public class CategoryWorkerService : BackgroundService
    {
        private readonly ILogger<CategoryWorkerService> _logger;
        private readonly IEventStoreConnection _eventStoreConnection;
        private readonly IServiceProvider _serviceProvider;

        public CategoryWorkerService(
           ILogger<CategoryWorkerService> logger,
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
                await _eventStoreConnection.ConnectToPersistentSubscriptionAsync(ScreamConfigurations.CategoryStreamName, ScreamConfigurations.CategoryGroupName, EventAppeared, autoAck: false);

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
                IRepository<Category> _categoryRepository = (IRepository<Category>)scope.ServiceProvider.GetRequiredService(typeof(IRepository<Category>));
                Category category;
                switch (@event)
                {
                    case CategoryInsertedEvent categoryInsertedEvent:
                        var categoryInsertDto = AutoMapperConfiguration.Mapper.Map<CategoryInsertDto>(categoryInsertedEvent);
                        category = AutoMapperConfiguration.Mapper.Map<Category>(categoryInsertDto);
                        await _categoryRepository.InsertAsync(category);
                        //await categoryMongoRepository.AddAsync(category);
                        break;
                    case CategoryUpdatedEvent categoryUpdatedEvent:
                        var categoryUpdateDto = AutoMapperConfiguration.Mapper.Map<CategoryUpdatedEvent>(categoryUpdatedEvent);
                        category = AutoMapperConfiguration.Mapper.Map<Category>(categoryUpdateDto);
                        await _categoryRepository.UpdateAsync(category);
                        //await categoryMongoRepository.UpdateAsync(category);
                        break;
                    case CategoryDeletedEvent categoryDeletedEvent:
                        var categoryDeleteDto = AutoMapperConfiguration.Mapper.Map<CategoryDeletedEvent>(categoryDeletedEvent);
                        category = AutoMapperConfiguration.Mapper.Map<Category>(categoryDeleteDto);
                        await _categoryRepository.DeleteAsync(category);
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
