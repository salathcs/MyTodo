using Entities;
using Entities.Models;
using MyTodo_Users.Interfaces;

namespace MyTodo_Users.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly MyTodoContext context;

        public UsersRepository(MyTodoContext context)
        {
            this.context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User? GetById(long id)
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

        public void Delete(long id)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user is null)
            {
                return;
            }

            context.Users.Remove(user);
            context.SaveChanges();
        }
    }
}
