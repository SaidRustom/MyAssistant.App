using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class ChatMessageDto : IDto<ChatMessage>
{
    [Required]
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
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