using AutoMapper;
using DataTransfer.DataTransferObjects;
using Entities.Models;
using MyTodo_Users.Interfaces;
using MyUtilities.Exceptions;
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

        public UserDto GetById(long id)
        {
            var userDto = usersRepository.GetById(id);

            if (userDto is null)
            {
                throw new EntityNotFoundException($"User not found by id: {id}!");
            }

            return userDto;
        }

        public void Create(UserDto userDto)
        {
            var user = mapper.Map<User>(userDto);
            entityLoader.TryFillExtendedEntityFields(user);
            usersRepository.Create(user);
        }

        public void Update(UserDto userDto)
        {
            var user = usersRepository.GetEntityById(userDto.Id);

            if (user is null)
            {
                throw new EntityNotFoundException($"User not found by id: {userDto.Id}!");
            }

            mapper.Map(userDto, user);
            entityLoader.TryFillExtendedEntityFields(user);
            usersRepository.Update(user);
        }

        public void Delete(long id)
        {
            usersRepository.Delete(id);
        }
    }
}
