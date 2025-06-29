using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Features.Notifications.Create
{
    public class CreateNotificationCommand : Notification, IRequest<Guid>
    {

    }
}
