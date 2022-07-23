using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using MyAuth_lib.MyAuthPolicies.Requirements;
using static MyAuth_lib.Constants.AuthConstants;
using static MyAuth_lib.Constants.PolicyConstants;

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
            if (policyName.StartsWith(RESOURCE_BASED_PREFIX))
            {
                var resourceBasedPolicy = new AuthorizationPolicyBuilder();
                resourceBasedPolicy.AddRequirements(new ResourceBasedReq());
                return Task.FromResult(resourceBasedPolicy.Build());
            }

            var policy = new AuthorizationPolicyBuilder();
            policy.AddRequirements(new ClientJwtAuthReq(policyName));       // Policy name need to be sent to the validation service
            return Task.FromResult(policy.Build());
        }
    }
}
