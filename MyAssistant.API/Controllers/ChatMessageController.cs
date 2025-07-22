using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers;

public class ChatMessageController(IMediator mediator, IMapper mapper) :
    MyAssistantBaseController(mediator, mapper)
{
    [HttpGet("{id}/{beforeMessageId?}")]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get(Guid id, Guid? beforeMessageId = null)
        => await ExecuteAsync<GetConversationQuery, ICollection<ChatMessageDto>>
        (new GetConversationQuery() { OtherUserId = id, BeforeMessageId = beforeMessageId},
            result => Ok(new ApiResponse<ICollection<ChatMessageDto>>(result, "Success")));
    
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<ShoppingListDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
        => await ExecuteAsync<GetConversationListQuery, ICollection<ConversationListItemDto>>
        (new GetConversationListQuery(),
            result => Ok(new ApiResponse<ICollection<ConversationListItemDto>>(result, "Success")));
    
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody]CreateChatMessageCommand command)
        => await CreateAsync<ChatMessage, Guid>(command);

}