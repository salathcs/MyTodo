using Microsoft.AspNetCore.Authorization;

namespace MyAuth_lib.MyAuthPolicies.Requirements
{
    public class PermissionReq : IAuthorizationRequirement
    {
        public PermissionReq(string permissionName)
        {
            PermissionName = permissionName;
        }

        public string PermissionName { get; set; }
    }
}
