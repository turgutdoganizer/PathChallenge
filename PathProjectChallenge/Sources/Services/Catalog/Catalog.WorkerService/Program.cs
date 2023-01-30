using Autofac.Extensions.DependencyInjection;
using Catalog.Core.EventSourcing;
using Catalog.Data.Infrastructure.Extensions;
using Catalog.Service.Infrastructure.Extensions;
using Catalog.WorkerService.Subscribers;
using Microsoft.AspNetCore.Builder;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Infrastructure;
using System.Reflection;

var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile(PathConfigurationDefaults.AppSettingsFilePath, true, true);
if (!string.IsNullOrEmpty(builder.Environment?.EnvironmentName))
{
    var path = string.Format(PathConfigurationDefaults.AppSettingsEnvironmentFilePath, builder.Environment.EnvironmentName);
    var b = builder.Configuration.AddJsonFile(path, true, true);
}
builder.Configuration.AddEnvironmentVariables();

//load application settings
builder.Services.ConfigureApplicationSettings(builder);

var appSettings = Singleton<AppSettings>.Instance;
var useAutofac = appSettings.Get<CommonConfig>().UseAutofac;

if (useAutofac)
    builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
else
    builder.Host.UseDefaultServiceProvider(options =>
    {
        //we don't validate the scopes, since at the app start and the initial configuration we need 
        //to resolve some services (registered as "scoped") through the root container
        options.ValidateScopes = false;
        options.ValidateOnBuild = true;
    });

//add services to the application and configure service provider
builder.Services.ConfigureApplicationServices(builder);





IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
       
        services.AddCustomMediatR();
        services.AddDI();
        services.AddDIEvent();
        services.AddDomainLevelValidation();
        services.AddEventStore(hostContext.Configuration);
        services.AddStreamDependencies();      
        services.AddHostedService<CategoryWorkerService>();
        services.AddHostedService<ProductWorkerService>();
    })
    .Build();

host.Run();
