using System.Security.Claims;

namespace IdentityServer.API.Core
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetSub(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst(x => x.Type.Equals("sub"))?.Value;
        }
        public static string GetEmail(this ClaimsPrincipal principal)
        {
            return principal?.FindFirst(x => x.Type.Equals("email"))?.Value;
        }
    }
}
