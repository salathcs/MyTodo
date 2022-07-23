using Entities.Base;
using System.Security.Claims;

namespace MyAuth_lib.Interfaces
{
    public interface IUserIdentityHelper
    {
        ClaimsIdentity? GetIdentity();
        long GetUserId();
        void TryFillExtendedEntityFields(BaseEntity entity);
    }
}