using IdentityServer.API.Controllers.Consent;

namespace IdentityServer.API.Controllers.Device
{
    public class DeviceAuthorizationInputModel : ConsentInputModel
    {
        public string UserCode { get; set; }
    }
}
