using DataTransfer.DataTransferObjects;

namespace MyTodo_Users.Interfaces
{
    public interface IManagerService
    {
        bool UpdateUserAndAddAdminRight(UserDto user);
        bool UpdateUserAndRemoveAdminRight(UserDto user);
    }
}