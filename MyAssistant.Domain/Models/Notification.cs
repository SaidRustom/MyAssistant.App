using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Models
{
    public class Notification : AuditableEntity
    { 
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(200)]
        public string? Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }

        public string? ActionUrl { get; set; }

        public bool EmailNotification { get; set; } 
    }

}
