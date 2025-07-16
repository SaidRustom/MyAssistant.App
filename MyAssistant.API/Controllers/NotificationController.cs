using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<NotificationDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
            => await GetAsync<Notification, NotificationDto>(id);

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedList<NotificationDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<PaginatedList<NotificationDto>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<PaginatedList<NotificationDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
            => await GetListAsync<Notification, NotificationDto>(pageNumber, pageSize);

        /// <summary>
        /// Creates a new Notification.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateOrUpdateNotificationCommand command)
            =>  await CreateAsync<Notification, Guid>(command);

        /// <summary>
        /// Updates an existing Notification.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(CreateOrUpdateNotificationCommand command)
            => await UpdateAsync<Notification, Guid>(command);
    }
}
