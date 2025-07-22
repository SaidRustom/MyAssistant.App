using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers;

public class ShoppingListItemController(IMediator mediator, IMapper mapper)
    : MyAssistantBaseController(mediator, mapper)
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListItemDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListItemDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListItemDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
        => await GetAsync<ShoppingListItem, ShoppingListItemDto>(id);
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody]CreateOrUpdateShoppingListItemCommand command)
        => await CreateAsync<ShoppingListItem, Guid>(command);
    
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(CreateOrUpdateShoppingListItemCommand command)
        => await ExecuteAsync<CreateOrUpdateShoppingListItemCommand, Guid>
            (command, result => Ok(new ApiResponse<Guid>(result, "Updated successfully.")));
}