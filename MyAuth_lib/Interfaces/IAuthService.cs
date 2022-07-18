using MyAuth_lib.Auth_Server.Models;

namespace MyAuth_lib.Interfaces
{
    public interface IAuthService
    {
        AuthResult Authenticate(AuthRequest authRequest);
    }
}