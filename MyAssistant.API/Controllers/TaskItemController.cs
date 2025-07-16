using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers
{
    public class TaskItemController : MyAssistantBaseController
    {
        public TaskItemController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }

        /// <summary>
        /// Retrieves a TaskItem by ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponse<TaskItemDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<TaskItemDto>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<TaskItemDto>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(Guid id)
            => await GetAsync<TaskItem, TaskItemDto>(id);

        [HttpGet]
        [ProducesResponseType(typeof(ApiResponse<PaginatedList<TaskItemDto>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse< PaginatedList<TaskItemDto>>), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse<PaginatedList<TaskItemDto>>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
            => await GetListAsync<TaskItem, TaskItemDto>(pageNumber, pageSize);

        /// <summary>
        /// Creates a new TaskItem.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create(CreateOrUpdateTaskItemCommand command)
            => await CreateAsync<TaskItem, Guid>(command);

        /// <summary>
        /// Updates an existing TaskItem.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update(CreateOrUpdateTaskItemCommand command)
            => await UpdateAsync<TaskItem, Guid>(command);
    }
}
