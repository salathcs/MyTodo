using Entities.Models;

namespace MyTodo_Users.Interfaces
{
    public interface IUsersRepository
    {
        void Create(User user);
        void Delete(long id);
        IEnumerable<User> GetAll();
        User? GetById(long id);
        void Update(User user);
    }
}