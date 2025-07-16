using AutoMapper;
using Azure;
using IdentityModel;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Features.ShoppingLists.Get;
using MyAssistant.Core.Features.ShoppingLists.GetList;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyAssistant.API.Controllers;

public class ShoppingListController(IMediator mediator, IMapper mapper) : 
    MyAssistantBaseController(mediator, mapper)
{
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id)
        => await ExecuteAsync<GetShoppingListQuery, ShoppingListDto>
                (new GetShoppingListQuery() {Id = id}, 
                result => Ok(new ApiResponse<ShoppingListDto>(result, "Success"))); 
    
    [ProducesResponseType(typeof(ApiResponse<List<ShoppingListDto>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<List<ShoppingListDto>>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<List<ShoppingListDto>>), StatusCodes.Status500InternalServerError)]
    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        =>  await ExecuteAsync<GetShoppingListPaginatedListQuery, PaginatedList<ShoppingListDto>>(
                new GetShoppingListPaginatedListQuery(pageNumber, pageSize),
                result => Ok(new ApiResponse<PaginatedList<ShoppingListDto>>(result)));
    
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