using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyTodo_Todos.Interfaces;
using MyUtilities.Interfaces;

namespace MyTodo_Todos.Services
{
    public class TodosService : ITodosService
    {
        private readonly ITodosRepository todosRepository;
        private readonly IExtendedEntityLoader entityLoader;
        private readonly IMapper mapper;

        public TodosService(ITodosRepository todosRepository, IExtendedEntityLoader entityLoader, IMapper mapper)
        {
            this.todosRepository = todosRepository;
            this.entityLoader = entityLoader;
            this.mapper = mapper;
        }

        public IEnumerable<TodoDto> GetAll()
        {
            return todosRepository.GetAll();
        }

        public TodoDto? GetById(long id)
        {
            return todosRepository.GetById(id);
        }

        public void Create(TodoDto todoDto)
        {
            var todo = mapper.Map<Todo>(todoDto);
            entityLoader.TryFillExtendedEntityFields(todo);
            todo.UserId = todoDto.UserId;  //UserId is ignored in mapper, but in create its required
            todosRepository.Create(todo);

            todoDto.Id = todo.Id;       //result
        }

        public bool Update(TodoDto todoDto)
        {
            var todo = todosRepository.GetEntityById(todoDto.Id);

            if (todo is null)
            {
                return false;
            }

            mapper.Map(todoDto, todo);
            entityLoader.TryFillExtendedEntityFields(todo);
            todosRepository.Update(todo);

            return true;
        }

        public bool Delete(long id)
        {
            var removed = todosRepository.Delete(id);

            return removed != null;
        }
    }
}
