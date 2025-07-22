using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Notifications.Create;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Core.Features.Notifications.Handle;

internal class HandleNotificationsCommandHandler<T> : IRequestHandler<HandleNotificationsCommand<T>>
where T : IEntityBase
{
    private readonly ILoggedInUserService _loggedInUserService;
    private readonly IMediator _mediator;
    private readonly INotificationRepository _repo;
    private readonly IBaseAsyncRepository<T> _entityRepo;

    public HandleNotificationsCommandHandler(ILoggedInUserService loggedInUserService, INotificationRepository repo,
        IMediator mediator, IBaseAsyncRepository<T> entityRepo)
    {
        _loggedInUserService = loggedInUserService;
        _repo = repo;
        _mediator = mediator;
        _entityRepo = entityRepo;
    }

    public async Task Handle(HandleNotificationsCommand<T> request, CancellationToken cancellationToken)
    {
        if (typeof(IShareable<T>).IsAssignableFrom(typeof(T)))
        {
            var entity = await _entityRepo.GetByIdAsync(request.EntityId) as IShareable<T>;

            if (string.IsNullOrEmpty(request.Message))
                request.Message = $"New updates on {entity.Title} may require your attention";

            //Notify the owner of the entity
            if (_loggedInUserService.UserId != entity.UserId && entity.NotifyOwnerOnChange)
            {
                await _mediator.Send(new CreateNotificationCommand(entity, entity.UserId, request.Message),cancellationToken);
            }
            
            //Then notify the people the entity is shared with
            foreach(var share in entity.Shares)
            {
                if (share.NotifyUserOnChange && share.UserId != _loggedInUserService.UserId)
                {
                    await _mediator.Send(new CreateNotificationCommand(entity, share.UserId, request.Message),cancellationToken);
                }
            }
        }

    }
}