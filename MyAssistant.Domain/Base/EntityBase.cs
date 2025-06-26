
using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Base entity, uses Guid for Id
    /// </summary>
    [Serializable]
    public abstract class EntityBase : IEntityBase
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid UserId { get; set; } = default!;
    }
}
