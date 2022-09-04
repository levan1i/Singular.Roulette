using System.Configuration;
using IdentityModel.AspNetCore.OAuth2Introspection;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;

namespace Singular.Roulette.Api.Identity
{
    public static class DefendencyInjection
    {
        public static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;


                options.EmitStaticAudienceClaim = true;
            })
              .AddInMemoryIdentityResources(IdentityConfiguration.IdentityResources)
              .AddInMemoryApiScopes(IdentityConfiguration.ApiScopes)
              .AddInMemoryClients(IdentityConfiguration.Clients)
              .AddInMemoryApiResources(IdentityConfiguration.ApiResources)
              .AddResourceOwnerValidator<ResourceOwnerPasswordValidator>()
              .AddProfileService<UserProfileService>()
          
              .AddDeveloperSigningCredential();


         
            services.AddAuthentication("Bearer").AddIdentityServerAuthentication("Bearer", o =>
                 {
                     o.Authority = configuration["Identity:IdentityServerUrl"];
                     o.ApiName = configuration["Identity:ApiName"];






                 });
            // not recommended for production - you need to store your key material somewhere secure
            builder.AddDeveloperSigningCredential();


            return services;

        }
    }
}
