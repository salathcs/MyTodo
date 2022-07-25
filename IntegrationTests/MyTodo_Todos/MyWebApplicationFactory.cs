using Entities;
using IntegrationTests.MyTodo_Todos.Authentication;
using IntegrationTests.MyTodo_Todos.Authorization;
using IntegrationTests.MyTodo_Todos.Db;
using IntegrationTests.MyTodo_Todos.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MyLogger.Interfaces;

namespace IntegrationTests.MyTodo_Todos
{
    public class MyWebApplicationFactory<ReqHandler> : WebApplicationFactory<Program>
        where ReqHandler : IMockedReqHandler
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                //mock db (use inMem)
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<MyTodoContext>));

                services.Remove(descriptor);

                services.AddDbContext<MyTodoContext>(options =>
                {
                    options.UseInMemoryDatabase($"InMemoryDbForTesting{typeof(ReqHandler).Name}");
                });

                //mock auth
                services.AddAuthentication("Test")
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(
                        "Test", options => { });

                //reqHandlers
                services.AddScoped(typeof(IAuthorizationHandler), typeof(ReqHandler));

                //authMock
                services.AddTransient<IAuthenticationSchemeProvider, MockSchemeProvider>();

                var sp = services.BuildServiceProvider();

                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<MyTodoContext>();
                    var logger = scopedServices
                        .GetRequiredService<IMyLogger>();

                    db.Database.EnsureCreated();

                    try
                    {
                        DbInitializer.InitializeDbForTests(db);
                    }
                    catch (Exception e)
                    {
                        logger.Error($"An error occurred seeding the database with test messages. Error: {e.Message}", e);
                    }
                }
            });
        }
    }
}