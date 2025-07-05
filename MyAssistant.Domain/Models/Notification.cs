using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Domain.Models
{
    [Table("Notification")]
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
        
        public string? ObjectType { get; set; } 
        
        public Guid? ObjectId { get; set; }
        
    }

}
