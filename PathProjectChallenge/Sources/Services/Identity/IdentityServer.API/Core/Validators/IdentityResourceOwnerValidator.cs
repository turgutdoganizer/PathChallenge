using IdentityModel;
using IdentityServer.API.Data.Entities;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.API.Core.Validators
{
    public class IdentityResourceOwnerValidator : IResourceOwnerPasswordValidator
    {
        private readonly UserManager<ApplicationUser> _userManager;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        public IdentityResourceOwnerValidator(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        /// <summary>
        /// Validator 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var isExist = await _userManager.FindByEmailAsync(context.UserName);
            if (isExist == null)
                return;
            var passwordCheck = await _userManager.CheckPasswordAsync(isExist, context.Password);
            if (passwordCheck == false)
                return;
            context.Result = new GrantValidationResult(isExist.Id.ToString(), OidcConstants.AuthenticationMethods.Password);

        }
    }
}
