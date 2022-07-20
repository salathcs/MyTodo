using Entities.Base;

namespace MyUtilities.Interfaces
{
    public interface IExtendedEntityLoader
    {
        void TryFillExtendedEntityFields(BaseEntity entity);
    }
}