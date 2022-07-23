using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyAuth_lib.Interfaces;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Services
{
    public class CrudService : ICrudService
    {
        private readonly ICrudRepository crudRepository;
        private readonly IUserIdentityHelper userIdentityHelper;
        private readonly IMapper mapper;

        public CrudService(ICrudRepository todosRepository, IUserIdentityHelper userIdentityHelper, IMapper mapper)
        {
            this.crudRepository = todosRepository;
            this.userIdentityHelper = userIdentityHelper;
            this.mapper = mapper;
        }

        public IEnumerable<TodoDto> GetAll()
        {
            return crudRepository.GetAll();
        }

        public TodoDto? GetById(long id)
        {
            return crudRepository.GetById(id);
        }

        public void Create(TodoDto todoDto)
        {
            var todo = mapper.Map<Todo>(todoDto);
            userIdentityHelper.TryFillExtendedEntityFields(todo);

            todo.UserId = userIdentityHelper.GetUserId();  //UserId from context
            crudRepository.Create(todo);

            todoDto.Id = todo.Id;       //result
        }

        public bool Update(TodoDto todoDto)
        {
            var todo = crudRepository.GetEntityById(todoDto.Id);

            if (todo is null)
            {
                return false;
            }

            mapper.Map(todoDto, todo);
            userIdentityHelper.TryFillExtendedEntityFields(todo);
            crudRepository.Update(todo);

            return true;
        }

        public bool Delete(long id)
        {
            var removed = crudRepository.Delete(id);

            return removed != null;
        }
    }
}
