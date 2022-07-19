using Entities.Models;
using MyAuth_lib.Auth_Server.Models;
using MyAuth_lib.Interfaces;

namespace MyAuth.Repo
{
    public class IdentityRepository : IIdentityRepository
    {
        public User TryGetUser(AuthRequest authRequest)
        {
            throw new NotImplementedException();
        }
    }
}
