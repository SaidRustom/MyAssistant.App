using MediatR;

namespace MyAssistant.Shared.DTOs;

public class GetConversationQuery : IRequest<IList<ChatMessageDto>>
{
    public Guid OtherUserId { get; set; }
    
    /// <summary>
    /// The Id of the last message sent to the user. Messages prior to it will be loaded
    /// </summary>
    public Guid? BeforeMessageId { get; set; }
}