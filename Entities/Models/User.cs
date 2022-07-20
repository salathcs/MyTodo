using Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class User : ExtendedEntity
    {
        [MaxLength(32)]
        [Column("name")]
        public string Name { get; set; }

        [MaxLength(32)]
        [Column("email")]
        public string Email { get; set; }


        [ForeignKey(nameof(Identity))]
        [Column("identityId")]
        public long IdentityId { get; set; }
        public virtual Identity UserIdentity { get; set; }


        [InverseProperty("UserPermission_User")]
        public virtual ICollection<UserPermission> UserPermissions { get; set; }

        [InverseProperty("TodoUser")]
        public virtual ICollection<Todo> UserTodos { get; set; }
    }
}
