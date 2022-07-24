using DataTransfer.DataTransferObjects;

namespace MyTodo_EmailWorker.Interfaces
{
    internal interface IMyEmailSender
    {
        Task SendEmail(TodoWithEmailDto todo, string emailBody);
    }
}
