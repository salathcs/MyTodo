using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace MyTodo_Todos.Interfaces
{
    public interface ITodosRepository
    {
        void Create(Todo todo);
        void Delete(long id);
        IEnumerable<TodoDto> GetAll();
        TodoDto? GetById(long id);
        Todo? GetEntityById(long id);
        void Update(Todo todo);
    }
}