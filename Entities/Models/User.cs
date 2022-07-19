using Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User : ExtendedEntity
    {
        [MaxLength(32)]
        public string Name { get; set; }

        [MaxLength(32)]
        public string Email { get; set; }


        [ForeignKey(nameof(Identity))]
        public long IdentityId { get; set; }
        public virtual Identity UserIdentity { get; set; }
    }
}
