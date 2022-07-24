using DataTransfer.DataTransferObjects;

namespace MyTodo_EmailWorker.Interfaces
{
    internal interface IMyHttpClient
    {
        Task<IEnumerable<TodoWithEmailDto>> GetTodosByExpiration(int expiration);

        Task SendTodoIds(IEnumerable<long> todoIds);
    }
}
