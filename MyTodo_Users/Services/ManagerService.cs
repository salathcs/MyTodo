using AutoMapper;
using DataTransfer.DataTransferObjects;
using MyTodo_Users.Interfaces;
using static Entities.Constants.PermissionNames;

namespace MyTodo_Users.Services
{
    public class ManagerService : IManagerService
    {
        private readonly ICrudRepository crudRepository;
        private readonly IManagerRepository managerRepository;
        private readonly IMapper mapper;

        public ManagerService(ICrudRepository crudRepository, IManagerRepository managerRepository, IMapper mapper)
        {
            this.crudRepository = crudRepository;
            this.managerRepository = managerRepository;
            this.mapper = mapper;
        }

        public bool UpdateUserAndAddAdminRight(UserDto userDto)
        {
            //data and validation
            var user = crudRepository.GetEntityById(userDto.Id);

            if (user is null)
            {
                return false;
            }

            var permission = managerRepository.GetPermissionByName(ADMIN_PERMISSION);

            if (permission is null)
            {
                return false;
            }

            //changes made
            mapper.Map(userDto, user);

            crudRepository.Update(user);

            //only add if not exists
            if (!user.UserPermissions.Any(x => x.UserPermission_Permission.PermissionName.Equals(ADMIN_PERMISSION)))
            {
                managerRepository.CreateUserPermission(user, permission);
            }

            return true;
        }

        public bool UpdateUserAndRemoveAdminRight(UserDto userDto)
        {
            //data and validation
            var user = crudRepository.GetEntityById(userDto.Id);

            if (user is null)
            {
                return false;
            }

            var userPermission = user.UserPermissions.FirstOrDefault(x => x.UserPermission_Permission.PermissionName.Equals(ADMIN_PERMISSION));

            //changes made
            mapper.Map(userDto, user);

            crudRepository.Update(user);

            if (userPermission != null)
            {
                managerRepository.RemoveUserPermission(userPermission);
            }

            return true;
        }
    }
}
