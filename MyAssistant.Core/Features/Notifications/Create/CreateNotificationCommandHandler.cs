using MediatR;
using MyAssistant.Core.Contracts.Persistence;

namespace MyAssistant.Core.Features.Notifications.Create
{
    public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, Guid>
    {
        private readonly INotificationRepository _repository;
        private IMediator _mediator;

        public CreateNotificationCommandHandler(INotificationRepository repository, IMediator mediator)
        {
            _repository = repository;
            _mediator = mediator;
        }

        public async Task<Guid> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
        {
            request.Id = Guid.NewGuid();
            var notification = await _repository.AddAsync(request);

            return request.Id;
        }
    }
}
