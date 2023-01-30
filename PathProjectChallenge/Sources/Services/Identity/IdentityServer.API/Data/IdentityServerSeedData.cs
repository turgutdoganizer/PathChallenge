using IdentityServer.API.Core;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;

namespace IdentityServer.API.Data
{
    public static class IdentityServerSeedData
    {
        public static void Seed(ConfigurationDbContext context)
        {
            if (!context.Clients.Any())
            {
                foreach (var client in Config.Clients)
                {

                    context.Clients.Add(client.ToEntity());

                }
            }

            if (!context.ApiResources.Any())
            {
                foreach (var apiResource in Config.ApiResources)
                {
                    context.ApiResources.Add(apiResource.ToEntity());
                }
            }

            if (!context.ApiScopes.Any())
            {
                Config.ApiScopes.ToList().ForEach(apiscope =>
                {
                    context.ApiScopes.Add(apiscope.ToEntity());
                });
            }

            if (!context.IdentityResources.Any())
            {
                foreach (var identityResource in Config.IdentityResources)
                {
                    context.IdentityResources.Add(identityResource.ToEntity());
                }
            }

            context.SaveChanges();
        }
    }
}
