using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using MyAuth_lib.Exceptions;
using MyLogger.Interfaces;
using static MyAuth_lib.Constants.AuthConstants;

namespace MyAuth_lib.Auth_Server
{
    public abstract class AuthServerPolicyReqHandler<T> : AuthorizationHandler<AuthServerPolicyReq> where T : IAuthorizationRequirement
    {
        protected readonly IMyLogger logger;
        protected readonly HttpContext httpContext;

        public AuthServerPolicyReqHandler(IMyLogger logger, IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new ArgumentNullException(nameof(HttpContext));
            }

            httpContext = httpContextAccessor.HttpContext;

            this.logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AuthServerPolicyReq requirement)
        {
            try
            {
                var valid = ValidateReq(context, requirement);

                if (valid)
                {
                    context.Succeed(requirement);
                    return Task.CompletedTask;
                }
            }
            catch (PolicyIsNullException e)
            {
                logger.Error("Policy missing!", e);
            }

            context.Fail();

            return Task.CompletedTask;
        }

        protected virtual bool ValidateReq(AuthorizationHandlerContext context, AuthServerPolicyReq requirement)
        {
            // policy from other Api
            if (!httpContext.Request.Query.ContainsKey(QUERY_POLICY))
            {
                return false;
            }

            var policyNames = httpContext.Request.Query[QUERY_POLICY];

            //if no policy provided, the request is valid, cause the token is valid
            if (policyNames.Count == 1 && policyNames.First().Equals(WITHOUT_POLICY, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            var policies = GetAuthorizationPolicies(policyNames, requirement);

            return ValidateAllReq(context, policies);
        }

        protected virtual IEnumerable<AuthorizationPolicy> GetAuthorizationPolicies(StringValues policyNames, AuthServerPolicyReq requirement)
        {
            foreach (var policyName in policyNames)
            {
                var policy = requirement.AuthorizationOptions.GetPolicy(policyName);

                if (policy is null)
                {
                    throw new PolicyIsNullException($"Policy doeasn't exists: {policyName}; request validation failed!");
                }

                yield return policy;
            }
        }

        protected virtual bool ValidateAllReq(AuthorizationHandlerContext context, IEnumerable<AuthorizationPolicy> policies)
        {
            foreach (var policy in policies)
            {
                foreach (var req in policy.Requirements)
                {
                    if (req is T reqOfType)
                    {
                        if (!ValidateReq(context, reqOfType))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }


        protected abstract bool ValidateReq(AuthorizationHandlerContext context, T req);
    }
}
