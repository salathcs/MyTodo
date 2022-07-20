using Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Identity : BaseEntity
    {
        [MaxLength(32)]
        [Column("userName")]
        public string UserName { get; set; }

        [MaxLength(32)]
        [Column("password")]
        public string Password { get; set; }

        //one to one relationship
        public virtual User IdentityUser { get; set; }
    }
}
