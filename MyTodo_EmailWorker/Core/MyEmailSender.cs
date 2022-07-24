using DataTransfer.DataTransferObjects;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;
using MimeKit.Text;
using MyLogger.Interfaces;
using MyTodo_EmailWorker.Interfaces;

namespace MyTodo_EmailWorker.Core
{
    internal class MyEmailSender : IMyEmailSender
    {
        private readonly IMyLogger logger;
        private readonly IConfiguration configuration;

        public MyEmailSender(IMyLogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public async Task SendEmail(TodoWithEmailDto todo, string emailBody)
        {
            logger.Info($"Sending start! Email from todo: {todo.Email}");

            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(configuration.GetValue<string>("Mail_From")));
            email.To.Add(MailboxAddress.Parse(todo.Email));
            email.Subject = "MyTodo Reminder";
            email.Body = new TextPart(TextFormat.Plain) { Text = emailBody };

            // send email
            using var smtp = new SmtpClient();
            smtp.Connect(configuration.GetValue<string>("Mail_Host"), 587, SecureSocketOptions.StartTls);
            smtp.Authenticate(configuration.GetValue<string>("Mail_LoginName"), configuration.GetValue<string>("Mail_Password"));
            var response = await smtp.SendAsync(email);
            smtp.Disconnect(true);

            logger.Info($"Email sent! Response: {response}");
        }
    }
}
