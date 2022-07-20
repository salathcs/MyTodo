using Entities.Base;
using Microsoft.AspNetCore.Http;
using MyUtilities.Interfaces;

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
                var context = contextAccessor.HttpContext;
                if (context is null)
                {
                    return;
                }

                var userName = context.User.Identity?.Name ?? "Unknown";

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
    }
}
