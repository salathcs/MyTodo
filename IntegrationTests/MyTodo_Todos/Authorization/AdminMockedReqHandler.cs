using IntegrationTests.MyTodo_Todos.Interfaces;
using Microsoft.AspNetCore.Authorization;
using MyAuth_lib.Auth_Client;

namespace IntegrationTests.MyTodo_Todos.Authorization
{
    public class AdminMockedReqHandler : AuthorizationHandler<ClientJwtAuthReq>, IMockedReqHandler
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ClientJwtAuthReq requirement)
        {
            context.Succeed(requirement);

            return Task.CompletedTask;
        }
    }
}
