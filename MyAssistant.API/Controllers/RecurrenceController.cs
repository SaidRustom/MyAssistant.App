using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecurrenceController : MyAssistantBaseController
    {

        public RecurrenceController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }

        /// <summary>
        /// Creates a new TaskItem.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody]CreateRecurrenceCommand command)
            => await ExecuteAsync<CreateRecurrenceCommand, Guid>
                (command, result => Created(string.Empty, new ApiResponse<Guid>(result, "Created successfully.")));
    }
}
