using MassTransit;
using MassTransit.MultiBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Ordering.API.Extensions;
using Ordering.Core.EventSourcing;
using Ordering.Data.Contexts.EntityFrameworkCore;
using Ordering.Service.Extensions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddMongoDb(builder.Configuration["MongoDbSettings:ConnectionString"], "Order MongoDb Health", HealthStatus.Degraded)
    .AddDbContextCheck<OrderingContext>()
    .AddEventStore(builder.Configuration["ConnectionStrings:EventStore"]);





builder.Services.AddDbContext<OrderingContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(assemblyName: assemblyName);
    });
});
builder.Services.AddMassTransit(builder.Configuration);
builder.Services.AddMongoDbSettings(builder.Configuration);
builder.Services.AddGeneralConfigurations();
builder.Services.AddRepositories();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddEventStore(builder.Configuration);
builder.Services.AddStreamDependencies();
builder.Services.AddCustomMediatR();
builder.Services.AddDomainLevelValidation();

















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
