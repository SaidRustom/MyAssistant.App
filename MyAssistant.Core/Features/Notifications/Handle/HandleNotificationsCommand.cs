using MediatR;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Notifications.Handle;

public class HandleNotificationsCommand<T>(Guid entityId) : IRequest where T : IEntityBase
{
    public Guid EntityId { get; set; }
    
}