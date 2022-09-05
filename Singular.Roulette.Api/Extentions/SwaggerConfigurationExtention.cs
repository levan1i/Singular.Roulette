using Microsoft.OpenApi.Models;

namespace Singular.Roulette.Api.Extentions
{
    public static class SwaggerConfigurationExtention
    {

        public static IServiceCollection AddSwagger(this IServiceCollection services,IConfiguration configuration)
        {

            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments("./Singular.Roulette.Api.xml");
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "oauth2"
                    },
                    Scheme = "oauth2",
                    Name = "oauth2",
                    In = ParameterLocation.Header,

                    Flows = new OpenApiOAuthFlows
                    {

                        Password = new OpenApiOAuthFlow
                        {
                            TokenUrl = new Uri($"{configuration["Identity:IdentityServerUrl"]}/connect/token"),


                            Scopes = new Dictionary<string, string>
             {
                 { "Singular.Roulette.Api", "Full" }

             },



                        }

                    },
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "oauth2" }
                        },
                        new[] { "api1" }
                    }
                });
            });
            return services;

        }
    }
}
