using Ordering.Event.WorkerService.Subscribers;

namespace Ordering.Event.WorkerService.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddWorkerServices(this IServiceCollection services)
        {
            services.AddHostedService<OrderWorkerService>();

        }
    }
}
