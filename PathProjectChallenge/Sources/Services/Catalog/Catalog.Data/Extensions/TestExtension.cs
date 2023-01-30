using Catalog.Core.Domain.Catalog;
using FluentMigrator;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using PathProjectChallenge.Core.Caching;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Events;
using PathProjectChallenge.Core.Infrastructure;
using PathProjectChallenge.Data;
using PathProjectChallenge.Data.Configuration;
using PathProjectChallenge.Data.Mapping;

namespace Catalog.Data.Infrastructure.Extensions
{
    public static class Test2Extension
    {


        public static void AddDI(this IServiceCollection services)
        {

            // For Infrastructure
            var memoryCache = new MemoryCache(new MemoryCacheOptions());
            var typeFinder = new AppDomainTypeFinder();
            Singleton<ITypeFinder>.Instance = typeFinder;

            var mAssemblies = typeFinder.FindClassesOfType<AutoReversingMigration>()
                .Select(t => t.Assembly)
                .Distinct()
                .ToArray();


            var configurations = typeFinder
                    .FindClassesOfType<IConfig>()
                    .Select(configType => (IConfig)Activator.CreateInstance(configType))
                    .ToList();
            var appSettings = new AppSettings(configurations);
            appSettings.Update(new List<IConfig> { Singleton<DataConfig>.Instance });
            Singleton<AppSettings>.Instance = appSettings;
            services.AddSingleton(appSettings);

            services.AddSingleton<IDataProviderManager, DataProviderManager>();
            services.AddSingleton(serviceProvider =>
                serviceProvider.GetRequiredService<IDataProviderManager>().DataProvider);
            services.AddSingleton<IMappingEntityAccessor>(x => x.GetRequiredService<IPathDataProvider>());


            services.AddSingleton<IMemoryCache>(memoryCache);
            //services.AddSingleton<IStaticCacheManager, MemoryCacheManager>();
            services.AddSingleton(typeof(IStaticCacheManager), typeof(MemoryCacheManager));
            //services.AddSingleton<ILocker, MemoryCacheManager>();
            services.AddSingleton(typeof(ILocker), typeof(MemoryCacheManager));



            services.AddSingleton(typeof(IRepository<>), typeof(EntityRepository<>));
            ////services.AddScoped<IRepository<Category>, EntityRepository<Category>>();
        }


    }
}
