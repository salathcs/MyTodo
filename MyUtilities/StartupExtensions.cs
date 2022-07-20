using Microsoft.Extensions.DependencyInjection;
using MyUtilities.Helpers;
using MyUtilities.Interfaces;

namespace MyUtilities
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyUtilities(this IServiceCollection services)
        {
            services.AddScoped<IExtendedEntityLoader, ExtendedEntityLoader>();

            return services;
        }
    }
}
