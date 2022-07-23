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
    }
}
