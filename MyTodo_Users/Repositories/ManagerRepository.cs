using Entities;
using Entities.Models;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly MyTodoContext context;

        public ManagerRepository(MyTodoContext context)
        {
            this.context = context;
        }

        public Permission? GetPermissionByName(string permissionName)
        {
            return context.Permissions.FirstOrDefault(x => x.PermissionName.Equals(permissionName));
        }

        public void CreateUserPermission(User user, Permission permission)
        {
            context.UserPermissions.Add(new UserPermission
            {
                UserId = user.Id,
                PermissionId = permission.Id,
            });

            context.SaveChanges();
        }

        public void RemoveUserPermission(UserPermission userPermissions)
        {
            context.UserPermissions.Remove(userPermissions);

            context.SaveChanges();
        }
    }
}
