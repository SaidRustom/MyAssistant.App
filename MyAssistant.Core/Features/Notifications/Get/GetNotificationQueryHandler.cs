using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Notifications.Create;
using MyAssistant.Domain.Models;

namespace MyAssistant.Core.Features.Notifications.Get;

public class GetNotificationQueryHandler: IRequestHandler<GetNotificationQuery, Notification>
{
    private readonly INotificationRepository _repository;
    private IMediator _mediator;

    public GetNotificationQueryHandler(INotificationRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }

    public async Task<Notification> Handle(GetNotificationQuery request, CancellationToken cancellationToken)
    {
        var notification = await _repository.GetByIdAsync(request.Id);
        return notification;
    }
}