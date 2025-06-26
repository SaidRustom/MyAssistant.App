using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Describes who has shared access to a given entity.
    /// </summary>
    public class EntityShare : EntityBase
    {
        [Required]
        public Guid EntityId { get; protected set; }

        public virtual IEntityBase Entity { get; protected set; } = default!;

        [Required]
        [StringLength(100)]
        public string EntityType { get; protected set; } = default!;

        [Required]
        public Guid SharedWithUserId { get; protected set; } = default!;

        [Required]
        public int PermissionTypeCode { get; set; } // e.g., "Read,Write"

        public virtual PermissionType PermissionType { get; set; }

        public bool IsActive { get; set; }

        [Required]
        public DateTime SharedAt { get; protected set; } = DateTime.Now;

        public EntityShare() { }

        public EntityShare (IEntityBase entity, Guid sharedWithUser, PermissionType permissionType)
        {
            Entity = entity;
            EntityId = entity.Id;
            SharedWithUserId = sharedWithUser;
            EntityType = entity.GetType().Name;
        }
    }
}
