using System.ComponentModel.DataAnnotations;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Describes who has shared access to a given entity.
    /// </summary>
    public class EntityShare : EntityBase
    {
        [Required]
        public Guid EntityId { get; set; }

        [Required]
        [StringLength(100)]
        public string EntityType { get; set; } = default!;

        [Required]
        public Guid SharedWithUserId { get; set; } = default!;

        [Required]
        [StringLength(100)]
        public string? Permissions { get; set; } // e.g., "Read,Write"

        [Required]
        public DateTime SharedAt { get; set; } = DateTime.Now;

    }
}
