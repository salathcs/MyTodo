using Microsoft.AspNetCore.Authorization;

namespace MyAuth_lib.Auth_Client
{
    public class ClientJwtAuthReq : IAuthorizationRequirement
    {
        public string Policy { get; set; }

        public ClientJwtAuthReq(string policy)
        {
            Policy = policy;
        }
    }
}
