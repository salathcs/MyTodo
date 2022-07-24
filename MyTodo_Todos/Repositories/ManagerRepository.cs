using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataTransfer.DataTransferObjects;
using Entities;
using Entities.Models;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Repositories
{
    public class ManagerRepository : IManagerRepository
    {
        private readonly MyTodoContext context;
        private readonly IMapper mapper;

        public ManagerRepository(MyTodoContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public IEnumerable<TodoDto> GetByUserId(long userId)
        {
            return context.Todos.Where(x => x.UserId == userId).ProjectTo<TodoDto>(mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<TodoWithEmailDto> GetByExpiration(int expirationMinutes)
        {
            //emailSent false, Expiration is larger then current time, but smaller then the specified
            return context.Todos.Where(x => !(x.EmailSent ?? false) &&
            x.Expiration > DateTime.UtcNow &&
            x.Expiration < DateTime.UtcNow.AddMinutes(expirationMinutes))
                .ProjectTo<TodoWithEmailDto>(mapper.ConfigurationProvider).ToList();
        }

        public IEnumerable<Todo> GetTodosByIds(IEnumerable<long> todoIds)
        {
            return context.Todos.Where(x => todoIds.Contains(x.Id)).ToList();
        }

        public void CallSaveChanges()
        {
            context.SaveChanges();
        }
    }
}
