using System.Data;
using Microsoft.Extensions.Options;
using Winecellar.Application;
using Winecellar.Application.Common.Interfaces;
using Winecellar.Infrastructure;
using Winecellar.Infrastructure.Repositories;


namespace Winecellar.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddRepositories(IServiceCollection services)
        {
            services.AddScoped<IWineRepository, WineRepository>();

        }
    }
}
