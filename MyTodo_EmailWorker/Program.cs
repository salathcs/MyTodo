using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MyLogger;
using MyTodo_EmailWorker;
using MyTodo_EmailWorker.Core;
using MyTodo_EmailWorker.Interfaces;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "MyTodo Email worker";
    })
    .ConfigureServices(services =>
    {
        services.AddHostedService<EmailWorkerBackgroundService>();

        services.AddHttpClient();

        services.AddSingleton<IMyEmailSender, MyEmailSender>();
        services.AddSingleton<IMyHttpClient, MyHttpClient>();

        services.AddMyLogger();
    })
    .Build();

await host.RunAsync();