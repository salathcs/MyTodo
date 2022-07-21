using DataTransfer.DataTransferObjects;

namespace MyTodo_Users.Interfaces
{
    public interface IUsersService
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(long id);
        void Create(UserDto userDto);
        bool Update(UserDto userDto);
        bool Delete(long id);
    }
}