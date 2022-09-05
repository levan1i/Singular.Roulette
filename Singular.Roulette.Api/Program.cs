
using Microsoft.EntityFrameworkCore;

using Singular.Roulette.Repository;
using Singular.Roulette.Services.Abstractions;
using System.Text;
using Singular.Roulette.Api.Identity;
using Microsoft.OpenApi.Models;
using Singular.Roulette.Api.Extentions;
using Singular.Roulette.Common.Middlewares;
using Singular.Roulette.Services.Hubs;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration);


builder.Services.AddRepository(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddServices();
builder.Services.AddIdentity(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSignalR();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseStaticFiles();
    app.UseSwaggerUI(
        c =>
        {
            c.OAuthClientId("SwaggerUI");
            c.OAuthClientSecret("secret");
            c.InjectStylesheet("/swagger-ui/custom.css");
          
        }
        );
}

app.UseMiddleware<TokenSessionMiddleware>();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapHub<BetHub>("/BetHub");
app.MapControllers();
app.UseIdentityServer();
app.UseMiddleware<HeartbeetMiddleware>();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});
app.Run();
