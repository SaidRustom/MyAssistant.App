using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Models
{
    [Table("ChatMessage")]
    public class ChatMessage : AuditableEntity
    {
        [Required]
        public Guid ReceiverUserId { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        public DateTime SentAt { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }

        [StringLength(200)]
        public string? AttachmentUrl { get; set; }

        [StringLength(100)]
        public string? MessageType { get; set; } 
    }
}
