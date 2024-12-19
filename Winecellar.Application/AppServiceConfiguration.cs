using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Winecellar.Application.Common.Validators.Identity;

namespace Winecellar.Application
{
    public static class AppServiceConfiguration
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddValidatorsFromAssemblyContaining<RegisterUserRequestDtoValidator>();

            return services;
        }
    }
}
