using Entities.Models;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;

        public UsersService(IUsersRepository usersRepository)
        {
            this.usersRepository = usersRepository;
        }

        public IEnumerable<User> GetAll()
        {
            return usersRepository.GetAll();
        }

        public User? GetById(long id)
        {
            return usersRepository.GetById(id);
        }

        public void Create(User user)
        {
            usersRepository.Create(user);
        }

        public void Update(User user)
        {
            usersRepository.Update(user);
        }

        public void Delete(long id)
        {
            usersRepository.Delete(id);
        }
    }
}
