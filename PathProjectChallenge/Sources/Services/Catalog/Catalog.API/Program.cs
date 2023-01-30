using Autofac.Extensions.DependencyInjection;
using Catalog.Core.EventSourcing;
using Catalog.Core.EventSourcing.Streams;
using Catalog.Data.EventSourcing.Streams;
using Catalog.Service.Infrastructure.Extensions;
using Catalog.Service.Infrastructure.PipelineBehaviours;
using MediatR;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using PathProjectChallenge.Core.Configuration;
using PathProjectChallenge.Core.Infrastructure;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
        options.ValidateScopes = false;
        options.ValidateOnBuild = true;
    });

//add services to the application and configure service provider
builder.Services.ConfigureApplicationServices(builder);


builder.Services.AddHealthChecks()
    .AddEventStore(builder.Configuration["ConnectionStrings:EventStore"]);




builder.Services.AddHttpContextAccessor();
builder.Services.AddCustomMediatR();
builder.Services.AddDomainLevelValidation();
builder.Services.AddStreamDependencies();
builder.Services.AddEventStore(builder.Configuration);







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
