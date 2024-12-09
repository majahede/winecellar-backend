using Microsoft.Extensions.Configuration;
using Serilog;
using Winecellar;
using Winecellar.Api.DependencyInjection;
using Winecellar.Api.Middlewares;
using Winecellar.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Winecellar.Application;
using Microsoft.Extensions.Options;
using Winecellar.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("Properties/appsettings.json")
    .AddJsonFile($"Properties/appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .Build();

var services = builder.Services;

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

services.AddCors(options => options.AddPolicy("_AllowedSpecificOrigins", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

services.AddDbContext<AppDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")));

DependencyInjection.AddRepositories(services);

services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));
services.AddSingleton(provider => provider.GetRequiredService<IOptions<ConnectionStrings>>().Value);

services.AddApplication();

builder.Host.UseSerilog((context, configuration) =>
    configuration.ReadFrom.Configuration(context.Configuration));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseMiddleware<ExceptionMiddleware>();

app.UseCors("_AllowedSpecificOrigins");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
