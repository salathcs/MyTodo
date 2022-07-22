using MyAuth.Interfaces;
using MyAuth.Models;
using MyAuth_lib.Auth_Server.Models;
using MyLogger.Interfaces;
using System.Web;

namespace MyAuth.Services
{
    public class LoginService : ILoginService
    {
        private readonly IMyLogger logger;
        private readonly IConfiguration configuration;

        public LoginService(IMyLogger logger, IConfiguration configuration)
        {
            this.logger = logger;
            this.configuration = configuration;
        }

        public ExtendedAuthResult CreateExtendedAuthResult(AuthResult authResult)
        {
            string redirectUrl = configuration.GetValue<string>("MyTodoAppUrl");
            logger.Debug($"MyTodoAppUrl: {redirectUrl}");

            var uriBuilder = new UriBuilder(redirectUrl);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["name"] = authResult.Name;
            query["userId"] = authResult.UserId.ToString();
            query["expiration"] = (authResult.Expiration?.Ticks ?? 0).ToString();
            query["token"] = authResult.Token;
            uriBuilder.Query = query.ToString();

            redirectUrl = uriBuilder.ToString();

            logger.Debug($"MyTodoAppUrl with query: {redirectUrl}");

            return new ExtendedAuthResult(authResult, redirectUrl);
        }
    }
}
