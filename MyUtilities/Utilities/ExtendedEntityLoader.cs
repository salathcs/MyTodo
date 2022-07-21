using Entities.Base;
using Microsoft.AspNetCore.Http;
using MyUtilities.Interfaces;
using System.Security.Claims;

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

        private ClaimsIdentity? GetIdentity()
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
