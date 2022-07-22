using Entities.Base;
using Microsoft.AspNetCore.Http;
using MyUtilities.Exceptions;
using MyUtilities.Interfaces;
using System.Security.Claims;
using static MyAuth_lib.Constants.ClaimConstants;

namespace MyUtilities.Helpers
{
    public class ExtendedEntityLoader : IExtendedEntityLoader
    {
        private readonly IHttpContextAccessor contextAccessor;

        public ExtendedEntityLoader(IHttpContextAccessor contextAccessor)
        {
            this.contextAccessor = contextAccessor;
        }

        public void TryFillExtendedEntityFields(BaseEntity entity)
        {
            if (entity is ExtendedEntity extendedEntity)
            {
                var identity = GetIdentity();
                if (identity is null)
                {
                    return;
                }

                var userName = identity.Name ?? "Unknown";

                //Create
                if (string.IsNullOrWhiteSpace(extendedEntity.CreatedBy))
                {
                    extendedEntity.Created = DateTime.UtcNow;
                    extendedEntity.CreatedBy = userName;
                }

                //Update
                extendedEntity.Updated = DateTime.UtcNow;
                extendedEntity.UpdatedBy = userName;
            }
        }
        public long GetUserId()
        {
            var identity = GetIdentity();

            if (identity is null)
            {
                throw new IdentityNotFoundException("Identity not found for Todo creation!");
            }

            var userIdClaim = identity.FindFirst(x => x.Type.Equals(IDENTIFIER));

            if (userIdClaim is null)
            {
                throw new IdentityNotFoundException("UserId claim not found!");
            }

            if (!long.TryParse(userIdClaim.Value, out long id))
            {
                throw new IdentityNotFoundException("UserIdClaim claim value is not a long!");
            }

            return id;
        }

        public ClaimsIdentity? GetIdentity()
        {
            var context = contextAccessor.HttpContext;
            if (context is null)
            {
                return null;
            }

            return context.User.Identities.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.Name ?? ""));
        }
    }
}
