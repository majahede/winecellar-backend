using Winecellar.Application.Common.Interfaces;
using Winecellar.Infrastructure.Repositories;


namespace Winecellar.Api
{
    public static class ServiceCongfiguration
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IIdentityRepository, IdentityRepository>();
            services.AddScoped<IWineRepository, WineRepository>();
            return services;

        }
    }
}
