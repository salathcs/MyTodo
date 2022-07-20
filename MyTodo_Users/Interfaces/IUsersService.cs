using DataTransfer.DataTransferObjects;
using Entities.Models;

namespace MyTodo_Users.Interfaces
{
    public interface IUsersService
    {
        void Create(UserDto userDto);
        void Delete(long id);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(long id);
        void Update(UserDto userDto);
    }
}