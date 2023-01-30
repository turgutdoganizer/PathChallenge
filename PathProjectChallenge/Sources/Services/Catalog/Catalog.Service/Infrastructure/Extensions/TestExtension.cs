using Catalog.Core.EventSourcing.Streams;
using Catalog.Data.EventSourcing.Streams;
using Catalog.Service.Infrastructure.Events;
using Catalog.Service.Infrastructure.PipelineBehaviours;
using FluentMigrator;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PathProjectChallenge.Core.Caching;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Events;
using PathProjectChallenge.Core.Infrastructure;
using PathProjectChallenge.Data.Configuration;
using PathProjectChallenge.Data.Mapping;
using PathProjectChallenge.Data;
using System.Reflection;
using Microsoft.Extensions.Caching.Memory;
using Catalog.Core.Domain.Catalog;
using Catalog.Service.Services.Categories.Commands;

namespace Catalog.Service.Infrastructure.Extensions
{
    public static class TestExtension
    {
        public static void AddCustomMediatR(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddMediatR(typeof(CategoryInsertCommand).GetTypeInfo().Assembly);
        }
        public static void AddDomainLevelValidation(this IServiceCollection services)
        {
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        }

        public static void AddStreamDependencies(this IServiceCollection services)
        {
            services.AddScoped<ICategoryStream, CategoryStream>();
            services.AddScoped<IProductStream, ProductStream>();
        }

        public static void AddDIEvent(this IServiceCollection services)
        {
            //services.AddSingleton<IEventPublisher, EventPublisher>();
            services.AddSingleton(typeof(IEventPublisher), typeof(EventPublisher));

        }


    }
}
