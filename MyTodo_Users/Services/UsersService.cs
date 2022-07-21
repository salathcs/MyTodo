using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyTodo_Users.Interfaces;
using MyUtilities.Interfaces;

namespace MyTodo_Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository usersRepository;
        private readonly IExtendedEntityLoader entityLoader;
        private readonly IMapper mapper;

        public UsersService(IUsersRepository usersRepository, IExtendedEntityLoader entityLoader, IMapper mapper)
        {
            this.usersRepository = usersRepository;
            this.entityLoader = entityLoader;
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
            entityLoader.TryFillExtendedEntityFields(user);
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
            entityLoader.TryFillExtendedEntityFields(user);
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
