using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace MyTodo_Users.Interfaces
{
    public interface IUsersRepository
    {
        void Create(User user);
        void Delete(long id);
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(long id);
        User? GetEntityById(long id);
        void Update(User user);
    }
}