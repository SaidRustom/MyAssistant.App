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

        public int MyAssistantServiceTypeCode { get; set; } // populated when the audit triggered by a background service..

        public DateTime DateTime { get; set; } = DateTime.Now;

        public virtual ICollection<HistoryEntry> HistoryEntries { get; set; } = new List<HistoryEntry>();

        public AuditLog() { }

        /// <summary>
        /// Creating a new instance from an EntityBase
        /// </summary>
        public AuditLog(string entityType, IEntityBase entity, Guid userId, int actionTypeCode)
        {
            Id = Guid.NewGuid();
            EntityType = entityType;
            EntityId = entity.Id;
            UserId = userId;
            ActionTypeCode = actionTypeCode;
        }

        /// <summary>
        /// The UserId used by all background services for audit (MyAssitantService)
        /// </summary>
        public readonly Guid MyAssistantServiceUserId = Guid.Parse("D25A2F08-BB7B-42B0-BC2C-7D1BFEDB76EC");  //TODO: Add the id to env variables

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditLog"/> class with the specified entity details and action context.
        /// Primarily used by background services to record audit trail events for entities processed by the system.
        /// </summary>
        public AuditLog(string entityType, IEntityBase entity, MyAssistantServiceType serviceType, int actionTypeCode)
        {
            Id = Guid.NewGuid();
            EntityType = entityType;
            EntityId = entity.Id;
            UserId = MyAssistantServiceUserId;
            MyAssistantServiceTypeCode = serviceType.Code;
            ActionTypeCode = actionTypeCode;
        }
    }
}
