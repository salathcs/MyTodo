using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;

namespace MyAuth_lib.Auth_Client
{
    public class FailingAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public FailingAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock) { }

        /// <summary>
        /// Client authentication must fail, the authentication is up for the auth server.
        /// </summary>
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return Task.FromResult(AuthenticateResult.Fail("Failed Authentication"));
        }
    }
}
