using Winecellar.Application;

namespace Winecellar.Api.DependencyInjection
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(IServiceCollection services)
        {
            services.AddApplication();
        }
    }
}
