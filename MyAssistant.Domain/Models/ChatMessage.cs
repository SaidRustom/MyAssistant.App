using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Models
{
    /// <summary>
    /// Represents a chat message entity exchanged between users.
    /// </summary>
    [Table("ChatMessage")]
    public class ChatMessage : EntityBase 
    {
        [Required]
        public Guid ReceiverUserId { get; set; }

        [Required]
        public string Content { get; set; } = default!;

        public DateTime SentAt { get; set; } = DateTime.Now;

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }

        [StringLength(200)]
        public string? AttachmentUrl { get; set; }

        [StringLength(100)]
        public string? MessageType { get; set; } 
    }
}
