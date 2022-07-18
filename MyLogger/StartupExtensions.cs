using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MyLogger.Interfaces;
using Serilog;

namespace MyLogger
{
    public static class StartupExtensions
    {
        /// <summary>
        /// Add my auth based on Serilog. 
        /// Requires appsettings.json with Serilog entry!
        /// </summary>
        /// <returns>Fluent pattern</returns>
        public static IServiceCollection AddMyLogger(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", true)
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();

            services.AddSingleton<IMyLogger, MyLogger>();

            return services;
        }
    }
}
