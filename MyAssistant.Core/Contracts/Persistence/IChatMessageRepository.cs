using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Contracts.Persistence;

public interface IChatMessageRepository : IBaseAsyncRepository<ChatMessage>
{
    Task<IList<ChatMessage>> GetListAsync(Guid otherUserId, Guid? beforeMessageId);
    Task<ICollection<Guid>> GetUserChatPartnersAsync();
    Task<int> GetUnreadCountAsync(Guid otherUserId);
    Task<DateTime> GetLastMessageWithUserAsync(Guid otherUserId);
}