using Microsoft.Extensions.DependencyInjection;
using MyAuth_lib.Auth_Server;
using MyAuth_lib.Interfaces;

namespace MyAuth_lib
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyAuthServer(this IServiceCollection services)
        {
            //service
            services.AddScoped<IAuthService, AuthService>();

            return services;
        }

        public static IServiceCollection AddMyAuthClient(this IServiceCollection services)
        {


            return services;
        }
    }
}
