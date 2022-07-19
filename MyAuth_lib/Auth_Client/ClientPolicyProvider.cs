using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth_lib.Auth_Client
{
    public class ClientPolicyProvider : IAuthorizationPolicyProvider
    {
        private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }

        public ClientPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // ASP.NET Core only uses one authorization policy provider, so if the custom implementation
            // doesn't handle all policies it should fall back to an alternate provider.
            BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);
        }

        /// <inheritdoc />
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() =>
            Task.FromResult(
            new AuthorizationPolicyBuilder()
            .AddRequirements(new ClientJwtAuthReq(WITHOUT_POLICY))          // Without policy only the token will be validated
            .Build());

        /// <inheritdoc />
        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync() =>
            BackupPolicyProvider.GetFallbackPolicyAsync();

        /// <inheritdoc />
        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new ClientJwtAuthReq(policyName));       // Policy name need to be sent to the validation service
            return Task.FromResult(policy.Build());
        }
    }
}
