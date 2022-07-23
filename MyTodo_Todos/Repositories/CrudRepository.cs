using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataTransfer.DataTransferObjects;
using Entities;
using Entities.Models;
using MyTodo_Todos.Interfaces;

namespace MyTodo_Todos.Repositories
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

        public IEnumerable<TodoDto> GetAll()
        {
            return context.Todos.ProjectTo<TodoDto>(mapper.ConfigurationProvider).ToList();
        }

        public TodoDto? GetById(long id)
        {
            return context.Todos.ProjectTo<TodoDto>(mapper.ConfigurationProvider).FirstOrDefault(x => x.Id == id);
        }

        public Todo? GetEntityById(long id)
        {
            return context.Todos.FirstOrDefault(x => x.Id == id);
        }

        public void Create(Todo todo)
        {
            context.Todos.Add(todo);
            context.SaveChanges();
        }

        public void Update(Todo todo)
        {
            context.Todos.Update(todo);
            context.SaveChanges();
        }

        public Todo? Delete(long id)
        {
            var todo = context.Todos.FirstOrDefault(x => x.Id == id);

            if (todo is null)
            {
                return null;
            }

            var removedEntry = context.Todos.Remove(todo);
            context.SaveChanges();

            return removedEntry.Entity;
        }
    }
}
