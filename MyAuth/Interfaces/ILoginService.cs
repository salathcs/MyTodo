using MyAuth.Models;
using MyAuth_lib.Auth_Server.Models;

namespace MyAuth.Interfaces
{
    public interface ILoginService
    {
        ExtendedAuthResult CreateExtendedAuthResult(AuthResult authResult);
    }
}