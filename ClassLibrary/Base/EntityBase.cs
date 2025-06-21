
using System.ComponentModel.DataAnnotations;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Base entity, uses Guid for Id
    /// </summary>
    public abstract class EntityBase
    {
        [Key]
        public Guid Id { get; set; }
    }
}
