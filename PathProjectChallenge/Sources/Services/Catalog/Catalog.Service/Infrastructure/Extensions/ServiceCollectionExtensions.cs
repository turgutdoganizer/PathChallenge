﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PathProjectChallenge.Core;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Infrastructure;
using System.Net;

namespace Catalog.Service.Infrastructure.Extensions
{
    /// <summary>
    /// Represents extensions of IServiceCollection
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Configure base application settings
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="builder">A builder for web applications and services</param>
        public static void ConfigureApplicationSettings(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            //let the operating system decide what TLS protocol version to use
            //see https://docs.microsoft.com/dotnet/framework/network-programming/tls
            ServicePointManager.SecurityProtocol = SecurityProtocolType.SystemDefault;

            //create default file provider
            CommonHelper.DefaultFileProvider = new PathFileProvider(builder.Environment);

            //register type finder
            var typeFinder = new WebAppTypeFinder();
            Singleton<ITypeFinder>.Instance = typeFinder;
            services.AddSingleton<ITypeFinder>(typeFinder);

            //add configuration parameters
            var configurations = typeFinder
                .FindClassesOfType<IConfig>()
                .Select(configType => (IConfig)Activator.CreateInstance(configType))
                .ToList();

            foreach (var config in configurations)
                builder.Configuration.GetSection(config.Name).Bind(config, options => options.BindNonPublicProperties = true);

            var appSettings = AppSettingsHelper.SaveAppSettings(configurations, CommonHelper.DefaultFileProvider, false);
            services.AddSingleton(appSettings);
        }


        /// <summary>
        /// Add services to the application and configure service provider
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="builder">A builder for web applications and services</param>
        public static void ConfigureApplicationServices(this IServiceCollection services,
            WebApplicationBuilder builder)
        {
            //add accessor to HttpContext
            services.AddHttpContextAccessor();

            //initialize plugins
            var mvcCoreBuilder = services.AddMvcCore();
            var pluginConfig = new PluginConfig();
            builder.Configuration.GetSection(nameof(PluginConfig)).Bind(pluginConfig, options => options.BindNonPublicProperties = true);
            mvcCoreBuilder.PartManager.InitializePlugins(pluginConfig);

            //create engine and configure service provider
            var engine = EngineContext.Create();

            engine.ConfigureServices(services, builder.Configuration);
        }

    }
}
