using Microsoft.Extensions.Configuration;
using MyAuth_lib.Interfaces;
using static MyAuth_lib.Constants.AuthServerConstans;

namespace MyAuth_lib.Auth_Server
{
    public class DefaultAuthServerSupplier : IAuthServerSupplier
    {
        private readonly int tokenExpiration;

        public DefaultAuthServerSupplier(IConfiguration configuration)
        {
            tokenExpiration = configuration.GetValue<int>(AUTH_SERVER_TOKEN_EXPIRATION);
        }

        public int GetTokenExpiration() => tokenExpiration;
    }
}
