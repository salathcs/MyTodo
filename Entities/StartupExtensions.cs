using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Entities
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddMyTodoContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<MyTodoContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
