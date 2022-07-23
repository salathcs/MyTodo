using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataTransfer.DataTransferObjects;
using Entities;
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
    }
}
