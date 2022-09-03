
using Microsoft.EntityFrameworkCore;

using Singular.Roulette.Repository;
using Singular.Roulette.Services.Abstractions;
using System.Text;
using Singular.Roulette.Api.Identity;
using Microsoft.OpenApi.Models;
using Singular.Roulette.Api.Extentions;
using Singular.Roulette.Common.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger(builder.Configuration);


builder.Services.AddRepository(builder.Configuration.GetConnectionString("Default"));
builder.Services.AddServices();
builder.Services.AddIdentity(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(
        c =>
        {
            c.OAuthClientId("SwaggerUI");
            c.OAuthClientSecret("secret");
        }
        );
}
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();

app.MapControllers();
app.UseIdentityServer();
app.UseMiddleware<ErrorHandlerMiddleware>();
app.Run();
