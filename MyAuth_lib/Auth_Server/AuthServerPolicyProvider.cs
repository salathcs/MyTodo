using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth_lib.Auth_Server
{
    public class AuthServerPolicyProvider
    {
        private DefaultAuthorizationPolicyProvider BackupPolicyProvider { get; }
        private AuthorizationOptions Options { get; }

        public AuthServerPolicyProvider(IOptions<AuthorizationOptions> options)
        {
            // ASP.Net has only one policy provider, and the custom doesn't handle all the policies
            // so the default provider needed
            BackupPolicyProvider = new DefaultAuthorizationPolicyProvider(options);

            Options = options.Value;
        }

        public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
        {
            return BackupPolicyProvider.GetDefaultPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
        {
            return BackupPolicyProvider.GetFallbackPolicyAsync();
        }

        public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            if (policyName.Equals(VALIDATION_POLICY, StringComparison.OrdinalIgnoreCase))
            {
                var policy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                policy.AddRequirements(new AuthServerPolicyReq(Options));
                return Task.FromResult(policy.Build());
            }
            else
            {
                return Task.FromResult(Options.GetPolicy(policyName));
            }
        }
    }
}
