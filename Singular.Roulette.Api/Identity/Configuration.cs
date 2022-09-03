using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Singular.Roulette.Api.Identity
{
    public class IdentityConfiguration
    {



        public static IEnumerable<ApiResource> ApiResources => new List<ApiResource>()
        {
             new ApiResource("Singular.Roulette.Api","Swagger  UI")
             {
                Scopes = new[] { "Singular.Roulette.Api" }
             },

         };
        public static IEnumerable<IdentityResource> IdentityResources =>
                       new IdentityResource[]
                       {
                         new IdentityResources.OpenId(),
                         new IdentityResources.Profile(),
                       };

        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {

                new ApiScope("Singular.Roulette.Api")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {


                new Client
                {
                    ClientId = "SwaggerUI",
                    ClientSecrets={ new Secret("secret".Sha256()) },
                     AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    AllowAccessTokensViaBrowser=true,
                    RequireClientSecret=false,

                    AllowOfflineAccess = true,
                    AllowedScopes = { "Singular.Roulette.Api" },
                  
                }
            };
    }


}
