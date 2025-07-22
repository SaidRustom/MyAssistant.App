using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Features.UserPreferences.GetAndCreateIfEmpty;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.API.Controllers;

public class UserPreferencesController : MyAssistantBaseController
{
    public UserPreferencesController(IMediator mediator, IMapper mapper) : base(mediator, mapper) { }

    /// <summary>
    /// Creates a new instance if none is found for the loggedin user
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<UserPreferencesDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<UserPreferencesDto>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<UserPreferencesDto>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Get()
        => await ExecuteAsync<GetUserPreferencesAndCreateIfEmpty, UserPreferencesDto>
                (new GetUserPreferencesAndCreateIfEmpty(),
                result => Ok(new ApiResponse<UserPreferencesDto>(result, "Success"))); 

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(UserPreferencesDto command)
    {
        return await UpdateAsync<UserPreferences, Guid>(command);
    }


}
