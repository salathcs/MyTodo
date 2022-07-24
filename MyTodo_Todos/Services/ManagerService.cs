using DataTransfer.DataTransferObjects;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Services
{
    public class ManagerService : IManagerService
    {
        private readonly IManagerRepository managerRepository;

        public ManagerService(IManagerRepository managerRepository)
        {
            this.managerRepository = managerRepository;
        }

        public IEnumerable<TodoDto> GetByUserId(long userId)
        {
            return managerRepository.GetByUserId(userId);
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
