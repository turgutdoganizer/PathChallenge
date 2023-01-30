using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Core.Data.Mongo;
using Ordering.Core.EventSourcing.Streams;
using Ordering.Core.Repositories;
using Ordering.Core.Repositories.Base;
using Ordering.Core.Utilities.Results.Abstract;
using Ordering.Data.EventStore;
using Ordering.Data.Repositories.Base;
using Ordering.Data.Repositories.SqlServerRepositories;
using Ordering.Service.Extensions.Pagination;
using Ordering.Service.Extensions.Pagination.Abstract;
using Ordering.Service.PipelineBehaviours;
using System.Reflection;

namespace Ordering.Service.Extensions
{
    public static class ServiceExtensions
    {
        //public static void AddMassTransit(this IServiceCollection services,IConfiguration configuration)
        //{
        //    services.AddMassTransit(x => {

        //        x.UsingRabbitMq((context, cfg) =>
        //        {
        //            cfg.Host(configuration.GetConnectionString("RabbitMQ"));

        //            //cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderRequestCompletedEventtQueueName, x =>
        //            //{
        //            //    x.ConfigureConsumer<OrderRequestCompletedEventConsumer>(context);
        //            //});

        //            //cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderRequestFailedEventtQueueName, x =>
        //            //{
        //            //    x.ConfigureConsumer<OrderRequestFailedEventConsumer>(context);
        //            //});
        //        });

        //    });
        //    services.AddMassTransitHostedService();
        //    services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
        //    services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
        //    services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());

        //}


        public static void AddGeneralConfigurations(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddSingleton<IPaginationUriService>(opt =>
            {
                var httpContextAccessor = opt.GetRequiredService<IHttpContextAccessor>();
                return new PaginationUriService(httpContextAccessor);
            });
            services.AddSingleton(typeof(IPaginationUriService), typeof(PaginationUriService));
            services.AddSingleton(typeof(IPaginationQuery), typeof(PaginationQuery));
        }

        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(SqlServerRepository<>));
            services.AddScoped(typeof(IOrderRepository), typeof(OrderSqlServerRepository));
        }

        public static void AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }

        public static void AddDomainLevelValidation(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        public static void AddAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        public static void AddStreamDependencies(this IServiceCollection services)
        {
            services.AddScoped<IOrderStream, OrderStream>();

        }

        public static IServiceCollection AddMongoDbSettings(this IServiceCollection services,
           IConfiguration configuration)
        {
            return services.Configure<MongoDbSettings>(options =>
            {
                options.ConnectionString = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.ConnectionStringValue).Value;
                options.Database = configuration
                    .GetSection(nameof(MongoDbSettings) + ":" + MongoDbSettings.DatabaseValue).Value;
            });
        }
    }
}
