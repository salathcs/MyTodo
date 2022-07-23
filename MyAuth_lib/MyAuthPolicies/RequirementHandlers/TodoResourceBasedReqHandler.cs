using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using MyAuth_lib.Interfaces;
using MyAuth_lib.MyAuthPolicies.Requirements;
using static Entities.Constants.PermissionNames;

namespace MyAuth_lib.MyAuthPolicies.RequirementHandlers
{
    public class TodoResourceBasedReqHandler : AuthorizationHandler<ResourceBasedReq, TodoDto>
    {
        private readonly IUserIdentityHelper userIdentityHelper;

        public TodoResourceBasedReqHandler(IUserIdentityHelper userIdentityHelper)
        {
            this.userIdentityHelper = userIdentityHelper;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceBasedReq requirement,
            TodoDto resource)
        {
            var identity = userIdentityHelper.GetIdentity();
            if (identity is null)
            {
                return Task.CompletedTask;
            }

            //admin hozzáférhet az erőforrásokhoz
            if (identity.HasClaim(x => x.Type.Equals(ADMIN_PERMISSION)))
            {
                context.Succeed(requirement);
            }
            //ha nem admin akkor ellenőrizni kell, hogy az erőforrás a sajátja
            else
            {
                var userId = userIdentityHelper.GetUserId();

                if (userId == resource.UserId)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
