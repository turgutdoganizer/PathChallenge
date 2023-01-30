using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using Ordering.Data.Contexts.EntityFrameworkCore;
using Ordering.Event.WorkerService;
using System.Reflection;
using Common.Logging;
using Serilog;
using Microsoft.EntityFrameworkCore;
using Ordering.Service.Extensions;
using Ordering.Core.EventSourcing;
using Ordering.Event.WorkerService.Extensions;

var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));


IHost host = Host.CreateDefaultBuilder(args).UseSerilog(SeriLogger.Configure)
    .ConfigureServices((hostContext, services) =>
    {

        services.AddDbContext<OrderingContext>(options =>
        {
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(assemblyName: assemblyName);
            });
        });
        services.AddGeneralConfigurations();
        services.AddMongoDbSettings(hostContext.Configuration);
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddEventStore(hostContext.Configuration);
        services.AddStreamDependencies();
        services.AddMultipleRepositories();
        services.AddRepositories();
        services.AddCustomMediatR();
        services.AddDomainLevelValidation();
        services.AddWorkerServices();

    })
    .Build();

await host.RunAsync();
