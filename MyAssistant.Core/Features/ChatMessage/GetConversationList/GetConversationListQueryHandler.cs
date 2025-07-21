using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ChatMessage.GetConversationList;

public class GetConversationListQueryHandler(IChatMessageRepository repo)
    : IRequestHandler<GetConversationListQuery, ICollection<ConversationListItemDto>>
{
    public async Task<ICollection<ConversationListItemDto>> Handle(GetConversationListQuery request, CancellationToken cancellationToken)
    {
        List<ConversationListItemDto> dtos = new();
        
        var chatPartners = await repo.GetUserChatPartnersAsync();

        foreach (var chatPartner in chatPartners)
        {
            ConversationListItemDto dto = new();
            dto.OtherUserId = chatPartner;
            dto.LastMessageSentAt = await repo.GetLastMessageWithUserAsync(chatPartner);
            dto.UnreadMessageCount = await repo.GetUnreadCountAsync(chatPartner);
            dtos.Add(dto); //TODO: Add Username/AvatarURl
        }
        
        return dtos;
    }
}