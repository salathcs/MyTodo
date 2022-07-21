using DataTransfer.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace DataTransfer
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MyDtoProfile));

            return services;
        }
        public static IServiceCollection AddMyAuthAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AuthProfile));

            return services;
        }
    }
}
