using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyAuth_lib.Interfaces;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IUserIdentityHelper userIdentityHelper;
        private readonly IMapper mapper;

        public UsersService(IUsersRepository usersRepository, IUserIdentityHelper userIdentityHelper, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.userIdentityHelper = userIdentityHelper;
            this.mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return usersRepository.GetAll();
        }

        public UserDto? GetById(long id)
        {
            return usersRepository.GetById(id);
        }

        public void Create(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            userIdentityHelper.TryFillExtendedEntityFields(user);
            usersRepository.Create(user);

            userDto.Id = user.Id;       //result
        }

        public bool Update(UserDto userDto)
        {
            var user = usersRepository.GetEntityById(userDto.Id);

            if (user is null)
            {
                return false;
            }

            mapper.Map(userDto, user);
            userIdentityHelper.TryFillExtendedEntityFields(user);
            usersRepository.Update(user);

            return true;
        }

        public bool Delete(long id)
        {
            var removed = usersRepository.Delete(id);

            return removed != null;
        }
    }
}
