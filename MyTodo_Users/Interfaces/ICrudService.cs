using DataTransfer.DataTransferObjects;

namespace MyTodo_Users.Interfaces
{
    public interface ICrudService
    {
        IEnumerable<UserDto> GetAll();
        UserDto? GetById(long id);
        void Create(UserWithIdentityDto userDto);
        bool Update(UserDto userDto);
        bool Delete(long id);
    }
}