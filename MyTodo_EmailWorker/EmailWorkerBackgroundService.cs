using DataTransfer.DataTransferObjects;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using MyLogger.Interfaces;
using MyTodo_EmailWorker.Exceptions;
using MyTodo_EmailWorker.Interfaces;

namespace MyTodo_EmailWorker
{
    internal class EmailWorkerBackgroundService : BackgroundService
    {
        private readonly IMyLogger logger;
        private readonly IConfiguration configuration;
        private readonly IMyEmailSender emailSender;
        private readonly IMyHttpClient httpClient;

        public EmailWorkerBackgroundService(IMyLogger logger, IConfiguration configuration, IMyEmailSender emailSender, IMyHttpClient httpClient)
        {
            this.logger = logger;
            this.configuration = configuration;
            this.emailSender = emailSender;
            this.httpClient = httpClient;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                while (!stoppingToken.IsCancellationRequested)
                {
                    logger.Debug("Started!");

                    await Process();

                    logger.Debug("Finished!");

                    await Task.Delay(TimeSpan.FromMinutes(configuration.GetValue<int>("TimerDelayMinutes")), stoppingToken);
                }
            }
            catch (Exception e)
            {
                logger.Error("Error occured!", e);

                Environment.Exit(1);
            }
        }

        private async Task Process()
        {
            try
            {
                //getTodos
                int todoBeforeExpireMinutes = configuration.GetValue<int>("TodoBeforeExpireMinutes");
                var todos = await httpClient.GetTodosByExpiration(todoBeforeExpireMinutes);

                if (!todos.Any())
                {
                    logger.Info("No Todos expires, process finished!");
                    return;
                }

                logger.Info($"Get todos succeeded! Count {todos.Count()}");

                //sentEmails
                var sentTodoIds = await SendMails(todos, todoBeforeExpireMinutes);

                if (!sentTodoIds.Any())
                {
                    logger.Info("No Todos email sent, process finished!");
                    return;
                }

                logger.Info($"Emails sent! Count {sentTodoIds.Count()}");

                //updateTodos
                await httpClient.SendTodoIds(sentTodoIds);
            }
            catch (EmailWorkerBaseException e)
            {
                logger.Error("Error in process!", e);
            }
        }

        private async Task<IEnumerable<long>> SendMails(IEnumerable<TodoWithEmailDto> todos, int todoBeforeExpireMinutes)
        {
            var sentTodoIds = new List<long>();

            foreach (var todo in todos)
            {
                try
                {
                    await emailSender.SendEmail(todo, $"Reminder, that your todo will be expired in {GetTodoExpirationMinutes(todo)} minutes!");

                    sentTodoIds.Add(todo.Id);
                }
                catch (EmailWorkerBaseException e)
                {
                    logger.Error("Error in mail sending!", e);
                }
            }

            return sentTodoIds;
        }

        private int GetTodoExpirationMinutes(TodoWithEmailDto todo)
        {
            if (todo.Expiration != null)
            {
                var counted = (todo.Expiration - DateTime.UtcNow);

                if (counted.HasValue)
                {
                    var minutesDouble = counted.Value.TotalMinutes;

                    return Convert.ToInt32(minutesDouble);
                }
            }

            return 0;
        }
    }
}
