using DataTransfer.DataTransferObjects;

namespace MyTodo_Todos.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<TodoDto> GetByUserId(long userId);
        void CreateTodoFor(long userId, TodoDto todoDto);
        IEnumerable<TodoWithEmailDto> GetByExpiration(int expirationMinutes);
        void UpdateEmailSentOn(IEnumerable<long> todoIds);
    }
}