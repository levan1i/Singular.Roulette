using Microsoft.EntityFrameworkCore;
using Singular.Roulette.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepository(builder.Configuration.GetConnectionString("Default"));
//builder.Services.AddDbContext<SingularDbContext>(options => options.UseMySQL(builder.Configuration.GetConnectionString("Default"),b=>b.MigrationsAssembly("Singular.Roulette.Repository")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
