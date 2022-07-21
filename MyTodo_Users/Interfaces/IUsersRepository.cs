using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace MyTodo_Users.Interfaces
{
    public interface IUsersRepository
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(long id);
        User? GetEntityById(long id);
        void Create(User user);
        void Update(User user);
        User? Delete(long id);
    }
}