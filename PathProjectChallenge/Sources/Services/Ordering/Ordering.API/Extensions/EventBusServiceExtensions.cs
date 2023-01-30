using MassTransit;
using Ordering.API.Consumers;
using EventBusRabbitMQ;
using ProtoBuf.Meta;

namespace Ordering.API.Extensions
{
    public static class EventBusServiceExtensions
    {
        public static void AddMassTransit(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<OrderPaymentCompletedRequestEventConsumer>();
                x.AddConsumer<OrderAccountBalanceIsInsufficientRequestEventConsumer>();
                x.AddConsumer<OrderPaymentFailedRequestEventConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(configuration.GetConnectionString("RabbitMQ"));
                   // cfg.UseHealthCheck(context); we should change healthcheck
                    
                    cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderPaymentCompletedRequestEvent, x =>
                    {
                        x.ConfigureConsumer<OrderPaymentCompletedRequestEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderAccountBalanceIsInsufficientRequestEvent, x =>
                    {
                        x.ConfigureConsumer<OrderAccountBalanceIsInsufficientRequestEventConsumer>(context);
                    });


                    cfg.ReceiveEndpoint(RabbitMQSettingsConst.OrderPaymentFailedRequestEvent, x =>
                    {
                        x.ConfigureConsumer<OrderPaymentFailedRequestEventConsumer>(context);
                    });

                });
            });
            //services.AddMassTransitHostedService();

            services.AddOptions<MassTransitHostOptions>()
            .Configure(options =>
            {
                // if specified, waits until the bus is started before
                // returning from IHostedService.StartAsync
                // default is false
                options.WaitUntilStarted = true;

                // if specified, limits the wait time when starting the bus
                options.StartTimeout = TimeSpan.FromSeconds(10);

                // if specified, limits the wait time when stopping the bus
                options.StopTimeout = TimeSpan.FromSeconds(30);
            });



            //services.AddSingleton<IBus>(provider => provider.GetRequiredService<IBusControl>());
            //services.AddSingleton<IPublishEndpoint>(provider => provider.GetRequiredService<IBusControl>());
            //services.AddSingleton<ISendEndpointProvider>(provider => provider.GetRequiredService<IBusControl>());

        }

    }
}
