using Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Permission : ExtendedEntity
    {
        [MaxLength(16)]
        [Column("permissionName")]
        public string PermissionName { get; set; }


        [InverseProperty("UserPermission_Permission")]
        public virtual ICollection<UserPermission> UserPermissions { get; set; }
    }
}
