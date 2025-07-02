using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Get;
using MyAssistant.Core.Features.Notifications.Create;
using MyAssistant.Core.Features.Notifications.Get;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;

namespace MyAssistant.API.Controllers
{
    public class NotificationController : MyAssistantBaseController
    {
        public NotificationController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Notification>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Notification>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Notification>), StatusCodes.Status500InternalServerError)]
        public Task<IActionResult> Get(Guid id)
            => GetAsync<GetEntityByIdQuery<Notification>, Notification>(new GetEntityByIdQuery<Notification>(id));

        /// <summary>
        /// Creates a new Notification.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateEntityCommand<Notification> command)
            => await CreateAsync<CreateEntityCommand<Notification>, Guid>(command);
    }
}
