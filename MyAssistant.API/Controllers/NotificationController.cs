using AutoMapper;
using Duende.IdentityServer.Extensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Get;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers
{
    public class NotificationController : MyAssistantBaseController
    {
        public NotificationController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }

        /// <summary>
        /// Retrieves a Notification by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<Notification>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Notification>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<Notification>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
            => await GetAsync<Notification, NotificationDto>(id);

        /// <summary>
        /// Creates a new Notification.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateNotificationCommand command)
            =>  await CreateAsync<Notification, Guid>(command);

        /// <summary>
        /// Updates an existing Notification.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(NotificationDto command)
            => await UpdateAsync<Notification, Guid>(command);
    }
}
