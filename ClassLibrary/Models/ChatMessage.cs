using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Models
{
    public class ChatMessage : AuditableEntity
    {
        [Required]
        public string SenderUserId { get; set; } = default!;

        [Required]
        public string ReceiverUserId { get; set; } = default!;

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
