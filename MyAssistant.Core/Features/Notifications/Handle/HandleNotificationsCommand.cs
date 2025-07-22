using MediatR;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Notifications.Handle;

internal class HandleNotificationsCommand<T>(Guid entityId, string? message = null) : IRequest where T : IEntityBase
{
    public Guid EntityId { get; set; } = entityId;
    
    public string? Message { get; set; } = message;
}