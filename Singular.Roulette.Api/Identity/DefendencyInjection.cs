using System.Configuration;
using System.Security.Cryptography.X509Certificates;
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

            //All Identity Configurations are extracted from the IdentityConfiguration class.
            //In case of requirements here is an option to move all configuration to the database side
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
                     //For SignalR
                     o.TokenRetriever = new Func<HttpRequest, string>(req =>
                     {
                         var fromHeader = TokenRetrieval.FromAuthorizationHeader();
                         var fromQuery = TokenRetrieval.FromQueryString();
                         return fromHeader(req) ?? fromQuery(req);
                     });




                 });
            //For Production Here is an option to use security sertificate
           // var x509 = new X509Certificate2(File.ReadAllBytes(filePath), "123123");
           // builder.AddSigningCredential(x509);
            builder.AddDeveloperSigningCredential();
           

            return services;

        }
    }
}
