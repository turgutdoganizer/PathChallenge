using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;


namespace IdentityServer.API.Core
{
    public static class Config
    {
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[]
        {
            new ApiResource("resource_gas_alarm_api")
            {
                Scopes = { "api_gas_alarm_full_permission" }
            },
            new ApiResource(IdentityServerConstants.LocalApi.ScopeName),
            new ApiResource("resource_gateway")
            {
                Scopes = { "gateway_full_permission" }
            },
            new ApiResource("MyAPI", "My Asp.net core WebApi,the best Webapi!"){
                    UserClaims =
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.Role,
                    }
                }
        };
        public static IEnumerable<IdentityResource> IdentityResources =>
                   new IdentityResource[]
                   {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResources.Phone(),
                new IdentityResource(){Name="Name" ,UserClaims= new [] {"name"}},
                   };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope("api_gas_alarm_full_permission","Full Permission for Gas Alarm API"),
                new ApiScope(IdentityServerConstants.LocalApi.ScopeName),
                new ApiScope("gateway_full_permission","Full Permission for Gateway")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                new Client
                {
                    ClientId = "ClientCredential",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) },
                    AllowedScopes = {
                        IdentityServerConstants.LocalApi.ScopeName,
                        "gateway_full_permission"
                    }
                },
                new Client
                {
                    ClientId = "DeviceClientCredential",
                    ClientName = "Client Credentials Client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F68A".Sha256()) },
                    AllowedScopes = { IdentityServerConstants.LocalApi.ScopeName }
                },
                new Client
                {
                    ClientId = "Angular-Client-Management",
                    ClientName = "Angular Resource Owner Password Client Credential",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "api_gas_alarm_full_permission",
                         "Roles",
                         "Name",
                         "gateway_full_permission"
                    },
                    AccessTokenLifetime = 604800,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                },
                new Client
                {
                    ClientId = "Flutter-Mobile-Application",
                    ClientName = "Flutter Resource Owner Password",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AllowOfflineAccess = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.Email,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.OfflineAccess,
                        IdentityServerConstants.LocalApi.ScopeName,
                        "api_gas_alarm_full_permission",
                         "Roles",
                         "Name",
                         "gateway_full_permission"
                    },
                    AccessTokenLifetime = 15120000,
                    UpdateAccessTokenClaimsOnRefresh = true,
                    RefreshTokenUsage = TokenUsage.ReUse,
                    RefreshTokenExpiration = TokenExpiration.Sliding,
                    SlidingRefreshTokenLifetime = (int)(DateTime.Now.AddDays(60)-DateTime.Now).TotalSeconds
                },
                new Client
                {
                    ClientName="Token Exchange Client",
                    ClientId="TokenExhangeClient",
                    ClientSecrets= {new Secret("secret".Sha256())},
                    AllowedGrantTypes= new []{ "urn:ietf:params:oauth:grant-type:token-exchange" },
                    AllowedScopes={ "discount_fullpermission", "payment_fullpermission", IdentityServerConstants.StandardScopes.OpenId }
                },
            };
    }
}
