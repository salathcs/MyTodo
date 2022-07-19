using Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Identity : BaseEntity
    {
        [MaxLength(32)]
        public string UserName { get; set; }

        [MaxLength(32)]
        public string Password { get; set; }


        [InverseProperty("UserIdentity")]
        public ICollection<User> Users { get; set; }
    }
}
