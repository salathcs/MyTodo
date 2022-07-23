using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyAuth_lib.Interfaces;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Services
{
    public class CrudService : ICrudService
    {
        private readonly ICrudRepository crudRepository;
        private readonly IUserIdentityHelper userIdentityHelper;
        private readonly IMapper mapper;

        public CrudService(ICrudRepository usersRepository, IUserIdentityHelper userIdentityHelper, IMapper mapper)
        {
            this.crudRepository = usersRepository;
            this.userIdentityHelper = userIdentityHelper;
            this.mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return crudRepository.GetAll();
        }

        public UserDto? GetById(long id)
        {
            return crudRepository.GetById(id);
        }

        public void Create(UserWithIdentityDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            userIdentityHelper.TryFillExtendedEntityFields(user);
            crudRepository.Create(user);

            userDto.Id = user.Id;       //result
        }

        public bool Update(UserDto userDto)
        {
            var user = crudRepository.GetEntityById(userDto.Id);

            if (user is null)
            {
                return false;
            }

            mapper.Map(userDto, user);
            userIdentityHelper.TryFillExtendedEntityFields(user);
            crudRepository.Update(user);

            return true;
        }

        public bool Delete(long id)
        {
            var removed = crudRepository.Delete(id);

            return removed != null;
        }
    }
}
