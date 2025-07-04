﻿using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Represents an audit event for an entity (user/date/action).
    /// </summary>
    public class AuditLog : EntityBase
    {
        [Required]
        public Guid EntityId { get; set; }

        [Required]
        [StringLength(100)]
        public string EntityType { get; set; } = default!;

        [Required]
        public DateTime EventDate { get; set; } = DateTime.Now;

        public int ActionTypeCode { get; set; } // e.g. "Create", "Update", "Delete"

        public virtual ICollection<HistoryEntry> HistoryEntries { get; set; } = new List<HistoryEntry>();

        public AuditLog() { }

        /// <summary>
        /// Creating a new instance requires an EntityBase
        /// </summary>
        /// <param name="entity"></param>
        public AuditLog(string entityType, IEntityBase entity, Guid userId, int actionTypeCode)
        {
            Id = Guid.NewGuid();
            EntityType = entityType;
            EntityId = entity.Id;
            UserId = userId;
            ActionTypeCode = actionTypeCode;
        }
    }
}
