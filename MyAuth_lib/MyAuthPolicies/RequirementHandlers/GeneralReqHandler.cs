using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using MyAuth_lib.Auth_Server;
using MyAuth_lib.MyAuthPolicies.Requirements;
using MyLogger.Interfaces;

namespace MyAuth_lib.MyAuthPolicies.RequirementHandlers
{
    public class GeneralReqHandler : AuthServerPolicyReqHandler<GeneralReq>
    {
        public GeneralReqHandler(IMyLogger logger, IHttpContextAccessor httpContextAccessor) : base(logger, httpContextAccessor)
        { }

        protected override bool ValidateReq(AuthorizationHandlerContext context, GeneralReq req)
        {
            return true;
        }
    }
}
