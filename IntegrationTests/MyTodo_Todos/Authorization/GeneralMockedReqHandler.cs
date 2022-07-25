using IntegrationTests.MyTodo_Todos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MyAuth_lib.Auth_Client;
using static MyAuth_lib.Constants.PolicyConstants;

namespace IntegrationTests.MyTodo_Todos.Authorization
{
    public class GeneralMockedReqHandler : AuthorizationHandler<ClientJwtAuthReq>, IMockedReqHandler
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientJwtAuthReq requirement)
        {
            if (!requirement.Policy.Equals(ADMIN_POLICY))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
