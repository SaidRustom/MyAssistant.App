using MediatR;

namespace MyAssistant.Shared.DTOs;

public class GetConversationListQuery : IRequest<ICollection<ConversationListItemDto>>
{
    
}