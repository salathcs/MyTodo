using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MyAuth_lib.Auth_Server;
using MyAuth_lib.MyAuthPolicies.Requirements;
using MyLogger.Interfaces;

namespace MyAuth_lib.MyAuthPolicies.RequirementHandlers
{
    public class PermissionReqHandler : AuthServerPolicyReqHandler<PermissionReq>
    {
        public PermissionReqHandler(IMyLogger logger, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        { }

        protected override bool ValidateReq(AuthorizationHandlerContext context, PermissionReq req)
        {
            return context.User.FindFirst(req.PermissionName) != null;
        }
    }
}
