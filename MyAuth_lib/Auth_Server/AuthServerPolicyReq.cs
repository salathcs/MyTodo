using Microsoft.AspNetCore.Authorization;

namespace MyAuth_lib.Auth_Server
{
    public class AuthServerPolicyReq : IAuthorizationRequirement
    {
        public AuthorizationOptions AuthorizationOptions { get; set; }

        public AuthServerPolicyReq(AuthorizationOptions authorizationOptions)
        {
            AuthorizationOptions = authorizationOptions;
        }
    }
}
