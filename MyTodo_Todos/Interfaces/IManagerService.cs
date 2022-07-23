using DataTransfer.DataTransferObjects;

namespace MyTodo_Todos.Interfaces
{
    public interface IManagerService
    {
        IEnumerable<TodoDto> GetByUserId(long userId);
    }
}