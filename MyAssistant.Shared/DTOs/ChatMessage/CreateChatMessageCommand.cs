using System.ComponentModel.DataAnnotations;
using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class CreateChatMessageCommand : IRequest<Guid>, IMapWith<ChatMessage>
{
    [Required]
    public Guid ReceiverUserId { get; set; }

    [Required]
    public string Content { get; set; } = default!;
    
    [StringLength(200)]
    public string? AttachmentUrl { get; set; }
    
}