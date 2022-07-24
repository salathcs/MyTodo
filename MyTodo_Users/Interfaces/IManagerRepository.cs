using Entities.Models;

namespace MyTodo_Users.Interfaces
{
    public interface IManagerRepository
    {
        Permission? GetPermissionByName(string permissionName);
        void CreateUserPermission(User user, Permission permission);
        void RemoveUserPermission(UserPermission userPermissions);
    }
}