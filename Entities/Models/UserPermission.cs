using Entities.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class UserPermission : BaseEntity
    {
        [ForeignKey(nameof(User))]
        [Column("userId")]
        public long UserId { get; set; }
        public virtual User UserPermission_User { get; set; }

        [ForeignKey(nameof(Permission))]
        [Column("permissionId")]
        public long PermissionId { get; set; }
        public virtual Permission UserPermission_Permission { get; set; }
    }
}
