using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Base
{
    public class ExtendedEntity : BaseEntity
    {
        [Column("created")]
        public DateTime Created { get; set; }

        [MaxLength(32)]
        [Column("createdBy")]
        public string CreatedBy { get; set; }

        [Column("updated")]
        public DateTime Updated { get; set; }

        [MaxLength(32)]
        [Column("updatedBy")]
        public string UpdatedBy { get; set; }
    }
}
