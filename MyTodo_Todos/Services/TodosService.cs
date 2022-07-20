using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyTodo_Todos.Interfaces;
using MyUtilities.Exceptions;
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

        public TodoDto GetById(long id)
        {
            var todoDto = todosRepository.GetById(id);

            if (todoDto is null)
            {
                throw new EntityNotFoundException($"Todo not found by id: {id}!");
            }

            return todoDto;
        }

        public void Create(TodoDto todoDto)
        {
            var todo = mapper.Map<Todo>(todoDto);
            entityLoader.TryFillExtendedEntityFields(todo);
            todosRepository.Create(todo);
        }

        public void Update(TodoDto todoDto)
        {
            var todo = todosRepository.GetEntityById(todoDto.Id);

            if (todo is null)
            {
                throw new EntityNotFoundException($"Todo not found by id: {todoDto.Id}!");
            }

            mapper.Map(todoDto, todo);
            entityLoader.TryFillExtendedEntityFields(todo);
            todosRepository.Update(todo);
        }

        public void Delete(long id)
        {
            todosRepository.Delete(id);
        }
    }
}
