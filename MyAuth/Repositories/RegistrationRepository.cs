using Entities;
using Entities.Models;
using MyAuth.Interfaces;

namespace MyAuth.Repositories
{
    public class RegistrationRepository : IRegistrationRepository
    {
        private readonly MyTodoContext context;

        public RegistrationRepository(MyTodoContext context)
        {
            this.context = context;
        }

        public void CreateUser(User user)
        {
            context.Users.Add(user);
            context.SaveChanges();
        }
    }
}
