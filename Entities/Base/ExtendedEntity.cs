using System.ComponentModel.DataAnnotations;

namespace Entities.Base
{
    public class ExtendedEntity : BaseEntity
    {
        public DateTime Created { get; set; }

        [MaxLength(32)]
        public string CreatedBy { get; set; }

        public DateTime Updated { get; set; }

        [MaxLength(32)]
        public string UpdatedBy { get; set; }
    }
}
