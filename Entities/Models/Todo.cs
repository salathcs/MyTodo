using Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Todo : ExtendedEntity
    {
        [MaxLength(32)]
        [Column("title")]
        public string Title { get; set; }

        [MaxLength]
        [Column("description")]
        public string? Description { get; set; }

        [Column("expiration")]
        public DateTime? Expiration { get; set; }


        [ForeignKey(nameof(User))]
        [Column("userId")]
        public long UserId { get; set; }
        public virtual User TodoUser { get; set; }
    }
}
