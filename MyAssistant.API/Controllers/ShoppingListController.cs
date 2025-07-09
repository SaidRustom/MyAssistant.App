using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers;

public class ShoppingListController(IMediator mediator, IMapper mapper) : 
    MyAssistantBaseController(mediator, mapper)
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
        => await GetAsync<ShoppingList, ShoppingListDto>(id);
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody]CreateOrUpdateShoppingListCommand command)
        => await CreateAsync<ShoppingList, Guid>(command);
    
    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update(CreateOrUpdateShoppingListCommand command)
        => await UpdateAsync<ShoppingList, Guid>(command);
}