using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.Domain.Entities
{
    public abstract class EntityBase<TEntityId>
    {
        [Key]
        [Column("id")]
        public TEntityId Id { get; set; }
    }
}
