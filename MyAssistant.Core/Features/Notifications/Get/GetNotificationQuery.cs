using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Features.Notifications.Get;

public class GetNotificationQuery : IRequest<Notification>
{
    public Guid Id { get; set; }
    
    public GetNotificationQuery(Guid id) => Id = id;
}