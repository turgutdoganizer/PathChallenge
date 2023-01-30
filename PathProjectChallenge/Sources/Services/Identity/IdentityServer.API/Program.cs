using IdentityServer.API.Core;
using IdentityServer.API.Core.Validators;
using IdentityServer.API.Data;
using IdentityServer.API.Data.Entities;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PathProjectChallenge.Logging;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog(SeriLogger.Configure);
builder.WebHost.UseUrls("http://localhost:5000", "https://localhost:5001");  // does not work
var assemblyName = typeof(Program).GetTypeInfo().Assembly.GetName().Name;
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddLocalApiAuthentication();

//builder.Services.AddSqlServer<ApplicationDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
    {
        sqlOptions.MigrationsAssembly(assemblyName: assemblyName);
    });
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
               .AddEntityFrameworkStores<ApplicationDbContext>()
               .AddDefaultTokenProviders();

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();



builder.Services.AddIdentityServer(options =>
{
    options.Events.RaiseErrorEvents = true;
    options.Events.RaiseInformationEvents = true;
    options.Events.RaiseFailureEvents = true;
    options.Events.RaiseSuccessEvents = true;
    options.EmitStaticAudienceClaim = true;
}).AddConfigurationStore(options =>
{
    options.ConfigureDbContext = c =>
        c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(assemblyName: assemblyName);
        });
}).AddOperationalStore(options =>
{
    options.ConfigureDbContext = c =>
        c.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(assemblyName: assemblyName);
        });
})
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddAspNetIdentity<ApplicationUser>()
                .AddResourceOwnerValidator<IdentityResourceOwnerValidator>()
                .AddExtensionGrantValidator<TokenExchangeExtensionGrantValidator>()
                .AddDeveloperSigningCredential(); // not recommended for production - you need to store your key material somewhere secure









Console.WriteLine("Hello1");
var seed = args.Contains("/seed");
if (seed)
{
    args = args.Except(new[] { "/seed" }).ToArray();
}

var app = builder.Build();

try
{
    if (seed)
    {
        Console.WriteLine("Hello");
        Log.Information("Seeding database...");
        var config = app.Services.GetRequiredService<IConfiguration>();
        var connectionString = config.GetConnectionString("DefaultConnection");
        SeedData.EnsureSeedData(connectionString);
        Log.Information("Done seeding database.");
    }
    using (var serviceScope = app.Services.CreateScope())
    {
        var services = serviceScope.ServiceProvider;

        var context = services.GetRequiredService<ConfigurationDbContext>();

        IdentityServerSeedData.Seed(context);
    }

}
catch (Exception ex)
{
    Log.Fatal(ex, "Host terminated unexpectedly.");
}











// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
