using Entities;
using Entities.Models;
using MyAuth_lib.Auth_Server.Models;
using MyAuth_lib.Interfaces;
using MyLogger.Interfaces;

namespace MyAuth.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly IMyLogger logger;
        private readonly MyTodoContext context;

        public IdentityRepository(IMyLogger logger, MyTodoContext context)
        {
            this.logger = logger;
            this.context = context;
        }

        public User? TryGetUser(AuthRequest authRequest)
        {
            return context.Identites.Where(x => x.UserName.Equals(authRequest.UserName) && x.Password.Equals(authRequest.Password))
                .FirstOrDefault()               //only one username password pair should exists
                ?.Users?.FirstOrDefault();      //only one User should have this Identity
        }
    }
}
