using System.ComponentModel.DataAnnotations;

namespace MyAssistant.Shared.DTOs;

public class ConversationListItemDto 
{
    [Required]
    public Guid OtherUserId { get; set; }
    
    public string OtherUserName { get; set; } = string.Empty;
    
    public string OtherUserAvatarUrl { get; set; } = string.Empty;
    
    public DateTime LastMessageSentAt { get; set; } = DateTime.Now;
    
    public int UnreadMessageCount { get; set; }

}