using Serilog;
using Winecellar.Api.Middlewares;
using Winecellar.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Winecellar.Application;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Winecellar.Infrastructure.Security;
using Winecellar.Api;
using Winecellar.Infrastructure.Security.Interfaces;
using Winecellar.Application.Common.Interfaces;
using Dapper;
using Winecellar.Infrastructure.DBConnection;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = builder.Configuration.SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
    .AddJsonFile("Properties/appsettings.json")
    .AddJsonFile($"Properties/appsettings.{builder.Environment.EnvironmentName}.json")
    .AddEnvironmentVariables()
    .Build();

DefaultTypeMap.MatchNamesWithUnderscores = true; 

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

services.AddSingleton(_ => DbConnectionFactory.CreateFactory(builder.Configuration.GetConnectionString("DbConnectionString")));

var tokenConfig = configurationBuilder.GetSection("TokenConfig");

builder.Services.AddAuthentication(opt => {
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenConfig.Get<TokenConfig>().Issuer,
            ValidAudience = tokenConfig.Get<TokenConfig>().Audience,
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenConfig
                    .Get<TokenConfig>()
                    .SecretKey))
        };
    });

services.AddScoped<ITokenHandler, Winecellar.Infrastructure.Security.TokenHandler>();
services.AddSingleton<IPasswordHandler, PasswordHandler>();
services.Configure<TokenConfig>(tokenConfig);

services.AddRepositories();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
