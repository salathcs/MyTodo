using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataTransfer.DataTransferObjects;
using Entities;
using Entities.Models;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Repositories
{
    public class CrudRepository : ICrudRepository
    {
        private readonly MyTodoContext context;
        private readonly IMapper mapper;

        public CrudRepository(MyTodoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<UserDto> GetAll()
        {
            return context.Users.ProjectTo<UserDto>(mapper.ConfigurationProvider).ToList();
        }

        public UserDto? GetById(long id)
        {
            return context.Users.ProjectTo<UserDto>(mapper.ConfigurationProvider).FirstOrDefault(x => x.Id == id);
        }

        public User? GetEntityById(long id)
        {
            return context.Users.FirstOrDefault(x => x.Id == id);
        }

        public void Create(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }

        public void Update(User user)
        {
            context.Users.Update(user);
            context.SaveChanges();
        }

        public User? Delete(long id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                return null;
            }

            var removedEntry = context.Users.Remove(user);
            context.Identites.Remove(user.UserIdentity);        //Also needs to be removed
            context.SaveChanges();

            return removedEntry.Entity;

        }
    }
}
