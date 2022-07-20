using DataTransfer.DataTransferObjects;

namespace MyTodo_Todos.Interfaces
{
    public interface ITodosService
    {
        void Create(TodoDto todoDto);
        void Delete(long id);
        IEnumerable<TodoDto> GetAll();
        TodoDto GetById(long id);
        void Update(TodoDto todoDto);
    }
}