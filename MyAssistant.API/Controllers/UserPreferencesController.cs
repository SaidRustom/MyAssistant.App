using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
    {
        var response =  await GetListAsync<UserPreferences, UserPreferencesDto>();
        if(response is ApiResponse<ICollection<UserPreferencesDto>> apiresponse)
        {
            if (apiresponse.Data?.Count == 0)
                 await CreateAsync<UserPreferences, Guid>(new UserPreferencesDto());

            return await GetListAsync<UserPreferences, UserPreferencesDto>();

        }

        return response;
    }

    [HttpPut]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(UserPreferencesDto command)
    {
        return await UpdateAsync<UserPreferences, Guid>(command);
    }


}
