using Entities.Models;
using MyAuth_lib.Auth_Server.Models;

namespace MyAuth_lib.Interfaces
{
    public interface IIdentityRepository
    {
        User? TryGetUser(AuthRequest authRequest);
    }
}
