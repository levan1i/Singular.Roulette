
using IdentityModel;
using IdentityServer4.Validation;
using Singular.Roulette.Services.Abstractions;

namespace Singular.Roulette.Api.Identity
{
    public class ResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {

        private readonly IUserService _userService;


        public ResourceOwnerPasswordValidator(IUserService userService) => _userService = userService;
        /// <inheritdoc/>

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {

            var validate =await _userService.ValidatePassword(context.UserName, context.Password);
            if(validate!=0)
            {
                context.Result = new GrantValidationResult(validate.ToString(), OidcConstants.AuthenticationMethods.Password);

            }
            else
            {
                context.Result = new GrantValidationResult(IdentityServer4.Models.TokenRequestErrors.InvalidRequest);
            }
           
        }
    }
}
