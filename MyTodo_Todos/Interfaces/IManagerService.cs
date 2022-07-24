using DataTransfer.DataTransferObjects;

namespace MyTodo_Todos.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<TodoDto> GetByUserId(long userId);
        IEnumerable<TodoDto> GetByExpiration(int expirationMinutes);
        void UpdateEmailSentOn(IEnumerable<long> todoIds);
    }
}