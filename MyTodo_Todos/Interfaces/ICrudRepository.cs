using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace MyTodo_Todos.Interfaces
{
    public interface ICrudRepository
    {
        IEnumerable<TodoDto> GetAll();
        TodoDto? GetById(long id);
        Todo? GetEntityById(long id);
        void Create(Todo todo);
        void Update(Todo todo);
        Todo? Delete(long id);
    }
}