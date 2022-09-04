using IdentityServer4.Models;
using IdentityServer4.Services;
using Singular.Roulette.Services.Abstractions;
using System.Security.Claims;

namespace Singular.Roulette.Api.Identity
{
    public class UserProfileService : IProfileService
    {

        private readonly IUserService _userService;

        public UserProfileService(IUserService userService)
        {
            _userService = userService;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {

            var user =await _userService.Get(Convert.ToInt64(context.Subject.Claims.First(x=>x.Type=="sub").Value));
            var sessionId = Guid.NewGuid().ToString();
            await _userService.AddUserHeartBeet(sessionId, user.UserId);
            var claims = new List<Claim>
            {
              new Claim("username",user.UserName ),
               new Claim("sessionId",sessionId ),
            };
            context.IssuedClaims = claims;

        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
           context.IsActive = true;
        }
    }
}
