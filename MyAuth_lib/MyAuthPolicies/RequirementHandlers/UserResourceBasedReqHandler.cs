using DataTransfer.DataTransferObjects;
using Microsoft.AspNetCore.Authorization;
using MyAuth_lib.Interfaces;
using MyAuth_lib.MyAuthPolicies.Requirements;
using static Entities.Constants.PermissionNames;

namespace MyAuth_lib.MyAuthPolicies.RequirementHandlers
{
    public class UserResourceBasedReqHandler : AuthorizationHandler<ResourceBasedReq, UserDto>
    {
        private readonly IUserIdentityHelper userIdentityHelper;

        public UserResourceBasedReqHandler(IUserIdentityHelper userIdentityHelper)
        {
            this.userIdentityHelper = userIdentityHelper;
        }

        protected override Task HandleRequirementAsync(
            AuthorizationHandlerContext context,
            ResourceBasedReq requirement,
            UserDto resource)
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

                if (userId == resource.Id)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
