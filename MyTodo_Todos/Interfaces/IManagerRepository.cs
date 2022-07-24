using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace MyTodo_Todos.Interfaces
{
    public interface IManagerRepository
    {
        IEnumerable<TodoDto> GetByUserId(long userId);
        IEnumerable<TodoDto> GetByExpiration(int expirationMinutes);
        IEnumerable<Todo> GetTodosByIds(IEnumerable<long> todoIds);
        void CallSaveChanges();
    }
}