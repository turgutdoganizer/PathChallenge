using Catalog.Service.Infrastructure;
using Catalog.Service.Infrastructure.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Infrastructure;

namespace Catalog.Event.WS.Infrastructure
{
    /// <summary>
    /// Represents the registering services on application startup
    /// </summary>
    public partial class PathStartup : IPathStartup
    {
        /// <summary>
        /// Add and configure any of the middleware
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="configuration">Configuration of the application</param>
        public virtual void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            //file provider
            services.AddScoped<IPathFileProvider, PathFileProvider>();



            ////plugins
            //services.AddScoped<IPluginService, PluginService>();
            //services.AddScoped<OfficialFeedManager>();

            //static cache manager
            var appSettings = Singleton<AppSettings>.Instance;
            //var distributedCacheConfig = appSettings.Get<DistributedCacheConfig>();
            //if (distributedCacheConfig.Enabled)
            //{
            //    switch (distributedCacheConfig.DistributedCacheType)
            //    {
            //        case DistributedCacheType.Memory:
            //            services.AddScoped<ILocker, MemoryDistributedCacheManager>();
            //            services.AddScoped<IStaticCacheManager, MemoryDistributedCacheManager>();
            //            break;
            //        case DistributedCacheType.SqlServer:
            //            services.AddScoped<ILocker, MsSqlServerCacheManager>();
            //            services.AddScoped<IStaticCacheManager, MsSqlServerCacheManager>();
            //            break;
            //        case DistributedCacheType.Redis:
            //            services.AddScoped<ILocker, RedisCacheManager>();
            //            services.AddScoped<IStaticCacheManager, RedisCacheManager>();
            //            break;
            //    }
            //}
            //else
            //{
            //    services.AddSingleton<ILocker, MemoryCacheManager>();
            //    services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            //}

            ////work context
            //services.AddScoped<IWorkContext, WebWorkContext>();

            ////store context
            //services.AddScoped<IStoreContext, WebStoreContext>();

            //services

            //services.AddScoped<ICategoryService, CategoryService>();


            //plugin managers
            //services.AddScoped(typeof(IPluginManager<>), typeof(PluginManager<>));
            //services.AddScoped<IAuthenticationPluginManager, AuthenticationPluginManager>();
            //services.AddScoped<IMultiFactorAuthenticationPluginManager, MultiFactorAuthenticationPluginManager>();
            //services.AddScoped<IWidgetPluginManager, WidgetPluginManager>();
            //services.AddScoped<IExchangeRatePluginManager, ExchangeRatePluginManager>();
            //services.AddScoped<IDiscountPluginManager, DiscountPluginManager>();
            //services.AddScoped<IPaymentPluginManager, PaymentPluginManager>();
            //services.AddScoped<IPickupPluginManager, PickupPluginManager>();
            //services.AddScoped<IShippingPluginManager, ShippingPluginManager>();
            //services.AddScoped<ITaxPluginManager, TaxPluginManager>();
            //services.AddScoped<ISearchPluginManager, SearchPluginManager>();

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            //register all settings
            var typeFinder = Singleton<ITypeFinder>.Instance;

            var settings = typeFinder.FindClassesOfType(typeof(ISettings), false).ToList();
            foreach (var setting in settings)
            {
                services.AddScoped(setting, serviceProvider =>
                {
                    return serviceProvider.GetRequiredService<ISettingService>();
                });
            }

            ////picture service
            //if (appSettings.Get<AzureBlobConfig>().Enabled)
            //    services.AddScoped<IPictureService, AzurePictureService>();
            //else
            //    services.AddScoped<IPictureService, PictureService>();

            //roxy file manager
            //services.AddScoped<IRoxyFilemanService, RoxyFilemanService>();
            //services.AddScoped<IRoxyFilemanFileProvider, RoxyFilemanFileProvider>();

            //installation service
            //services.AddScoped<IInstallationService, InstallationService>();

            ////slug route transformer
            //if (DataSettingsManager.IsDatabaseInstalled())
            //    services.AddScoped<SlugRouteTransformer>();

            //schedule tasks
            //services.AddSingleton<ITaskScheduler, TaskScheduler>();
            //services.AddTransient<IScheduleTaskRunner, ScheduleTaskRunner>();

            //event consumers
            //var consumers = typeFinder.FindClassesOfType(typeof(IConsumer<>)).ToList();
            //foreach (var consumer in consumers)
            //    foreach (var findInterface in consumer.FindInterfaces((type, criteria) =>
            //    {
            //        var isMatch = type.IsGenericType && ((Type)criteria).IsAssignableFrom(type.GetGenericTypeDefinition());
            //        return isMatch;
            //    }, typeof(IConsumer<>)))
            //        services.AddScoped(findInterface, consumer);

            //XML sitemap
            //services.AddScoped<IXmlSiteMap, XmlSiteMap>();

            //register the Lazy resolver for .Net IoC
            var useAutofac = appSettings.Get<CommonConfig>().UseAutofac;
            if (!useAutofac)
                services.AddScoped(typeof(Lazy<>), typeof(LazyInstance<>));
        }

        /// <summary>
        /// Configure the using of added middleware
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public void Configure(IApplicationBuilder application)
        {
        }

        /// <summary>
        /// Gets order of this startup configuration implementation
        /// </summary>
        public int Order => 2000;
    }
}
