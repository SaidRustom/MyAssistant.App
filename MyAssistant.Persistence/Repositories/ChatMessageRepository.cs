using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Models;
using MyAssistant.Persistence.Repositories.Base;

namespace MyAssistant.Persistence.Repositories;

public class ChatMessageRepository : BaseAsyncRepository<ChatMessage>, IChatMessageRepository
{
    private MyAssistantDbContext _context;
    private ILoggedInUserService _loggedInUserService;

    public ChatMessageRepository(MyAssistantDbContext context, ILoggedInUserService loggedInUserService) : base(context)
    {
        _context = context;
        _loggedInUserService = loggedInUserService;
    }

    public override Task<(IList<ChatMessage> Items, int TotalCount)> GetPagedListAsync(Guid userId, Expression<Func<ChatMessage, bool>> filter, int pageNumber, int pageSize)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Retrieves a list of chat messages between the current (logged-in) user and another specified user,
    /// optionally fetching only messages sent before a specific message (for pagination).
    /// </summary>
    /// <param name="otherUserId">The ID of the other user involved in the chat.</param>
    /// <param name="beforeMessageId">An optional message ID, to fetch messages sent before this message.</param>
    /// <returns>A list of chat messages between the two users, ordered chronologically (oldest to newest, up to 50 messages).</returns>
    public async Task<IList<ChatMessage>> GetListAsync(Guid otherUserId, Guid? beforeMessageId)
    {
        // Start by querying all chat messages exchanged between the logged-in user and the other user
        // Either sent or received by the logged-in user.
        var query = _context.ChatMessages
            .Where(
                (m => (m.UserId == _loggedInUserService.UserId && m.ReceiverUserId == otherUserId) ||
                      (m.ReceiverUserId == _loggedInUserService.UserId && m.UserId == _loggedInUserService.UserId)
                ));

        // If a "beforeMessageId" is provided, filter messages sent before the referenced message
        if (beforeMessageId.HasValue)
        {
            // Retrieve the message object for the specified message ID
            var beforeMessage = await _context.ChatMessages.FindAsync(beforeMessageId.Value);
            if (beforeMessage != null)
            {
                // Narrow down the query to only include messages sent before the timestamp of beforeMessage
                query = query.Where(m => m.SentAt < beforeMessage.SentAt);
            }
        }

        // Order the messages by sent time (newest first), then take up to 50 most recent messages
        var messages = await query
            .OrderByDescending(m => m.SentAt)
            .Take(50)
            .ToListAsync();

        // Reverse the list to present the messages in chronological order (oldest first)
        messages.Reverse();

        //Update the message record, set IsRead => true
        foreach (var message in messages.Where(m => m.UserId != _loggedInUserService.UserId && !m.IsRead))
        {
            message.IsRead = true;
            message.ReadAt = DateTime.Now;
            await _context.SaveChangesAsync();
        }

        return messages;
    }

    /// <summary>
    /// Gets the list of unique user IDs that the currently logged-in user has chatted with.
    /// </summary>
    /// <returns>A collection of unique user IDs.</returns>
    public async Task<ICollection<Guid>> GetUserChatPartnersAsync()
    {
        return await _context.ChatMessages
            .Where(m => m.UserId == _loggedInUserService.UserId || m.ReceiverUserId == _loggedInUserService.UserId)
            .Select(m => m.UserId == _loggedInUserService.UserId ? m.ReceiverUserId : m.UserId)
            .Distinct()
            .ToListAsync();
    }

    /// <summary>
    /// Returns the count of unread messages with otherUser
    /// </summary>
    /// <param name="otherUserId"></param>
    /// <returns></returns>
    public async Task<int> GetUnreadCountAsync(Guid otherUserId)
    {
        return await _context.ChatMessages
            .Where(m => m.UserId != _loggedInUserService.UserId && !m.IsRead)
            .CountAsync();
    }
    
    /// <summary>
    /// Returns the DateTime of last message sent with otherUser
    /// </summary>
    /// <param name="userId"></param>
    /// <returns></returns>
    public async Task<DateTime> GetLastMessageWithUserAsync(Guid otherUserId)
    {
        return await _context.ChatMessages
            .Where(m => m.UserId == otherUserId || m.UserId == _loggedInUserService.UserId)
            .OrderByDescending(m => m.SentAt)
            .Select(m => m.SentAt)
            .FirstOrDefaultAsync();
    }
}