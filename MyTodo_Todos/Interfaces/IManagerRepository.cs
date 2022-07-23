using DataTransfer.DataTransferObjects;

namespace MyTodo_Todos.Interfaces
{
    public interface IManagerRepository
    {
        IEnumerable<TodoDto> GetByUserId(long userId);
    }
}