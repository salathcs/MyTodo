using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyAuth_lib.Interfaces;
using MyLogger.Interfaces;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IMyLogger logger;
        private readonly IManagerRepository managerRepository;
        private readonly ICrudRepository crudRepository;
        private readonly IUserIdentityHelper userIdentityHelper;
        private readonly IMapper mapper;

        public ManagerService(IMyLogger logger, IManagerRepository managerRepository, ICrudRepository crudRepository, IUserIdentityHelper userIdentityHelper, IMapper mapper)
        {
            this.logger = logger;
            this.managerRepository = managerRepository;
            this.crudRepository = crudRepository;
            this.userIdentityHelper = userIdentityHelper;
            this.mapper = mapper;
        }

        public IEnumerable<TodoDto> GetByUserId(long userId)
        {
            return managerRepository.GetByUserId(userId);
        }

        public void CreateTodoFor(long userId, TodoDto todoDto)
        {
            var todo = mapper.Map<Todo>(todoDto);
            userIdentityHelper.TryFillExtendedEntityFields(todo);

            todo.UserId = userId;
            crudRepository.Create(todo);

            logger.Info($"Todo with id: {todo.Id} created by user with id: {userIdentityHelper.GetUserId()}!");

            todoDto.Id = todo.Id;       //result
        }

        public IEnumerable<TodoWithEmailDto> GetByExpiration(int expirationMinutes)
        {
            return managerRepository.GetByExpiration(expirationMinutes);
        }

        public void UpdateEmailSentOn(IEnumerable<long> todoIds)
        {
            var todos = managerRepository.GetTodosByIds(todoIds);

            foreach (var todo in todos)
            {
                todo.EmailSent = true;
            }

            managerRepository.CallSaveChanges();
        }
    }
}
