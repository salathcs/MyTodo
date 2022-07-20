using Microsoft.Extensions.Configuration;
using MyAuth_lib.Interfaces;
using static MyAuth_lib.Constants.AuthClientConstants;

namespace MyAuth_lib.Auth_Client
{
    public class DefaultAuthClientSupplier : IAuthClientSupplier
    {
        private readonly string validationUrl;
        private readonly int cacheExpiration;

        public DefaultAuthClientSupplier(IConfiguration configuration)
        {
            validationUrl = configuration.GetValue<string>(AUTH_SERVER_VALIDATION_URL);
            cacheExpiration = configuration.GetValue<int>(AUTH_CLIENT_REQUEST_VALID_CACHE_EXPIRATION);
        }

        public string GetValidationUrl() => validationUrl;

        public int GetCacheExpiration() => cacheExpiration;
    }
}
