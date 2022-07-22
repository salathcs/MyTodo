using Entities.Base;
using System.Security.Claims;

namespace MyUtilities.Interfaces
{
    public interface IExtendedEntityLoader
    {
        void TryFillExtendedEntityFields(BaseEntity entity);

        long GetUserId();

        ClaimsIdentity? GetIdentity();
    }
}