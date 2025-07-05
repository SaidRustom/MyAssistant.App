using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Core.Exceptions;
using MyAssistant.Core.Profiles;
using AutoMapper;
using MyAssistant.Shared;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Update;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using MyAssistant.Core.Features.Base.Get;

namespace MyAssistant.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public abstract class MyAssistantBaseController : ControllerBase
    {
        protected readonly IMediator Mediator;
        protected readonly IMapper Mapper;

        protected MyAssistantBaseController(IMediator mediator, IMapper mapper)
        {
            Mediator = mediator;
            Mapper = mapper;
        }

        // Unified execution
        protected async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(
            TRequest request,
            Func<TResponse, IActionResult> onSuccess) where TRequest : IRequest<TResponse>
        {
            try
            {
                var response = await Mediator.Send(request);
                return onSuccess(response);
            }
            catch (ValidationException validation)
            {
                return BadRequest(new ApiResponse<TResponse>(
                    validation.Errors,
                    "Validation failed."
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse<TResponse>(
                    new List<string> { ex.Message },
                    "Internal server error."
                ));
            }
        }

        [HttpGet("{id}")]
        protected virtual async Task<IActionResult> GetAsync<TEntity, TResponse>(Guid id)
            where TEntity : class, IEntityBase
            where TResponse : IDto<TEntity>
        {
            var query = new GetEntityByIdQuery<TEntity, TResponse>(id);

            return await ExecuteAsync<IRequest<TResponse>, TResponse>(
                query, result => Ok(new ApiResponse<TResponse>(result)));
        }

        [HttpPost]
        protected virtual async Task<IActionResult> CreateAsync<TEntity, TResponse>(IMapWith<TEntity> command)
            where TEntity : class, IEntityBase
        {
            var obj = Mapper.Map<TEntity>(command);
            var cmd = new CreateEntityCommand<TEntity>(obj) as IRequest<TResponse>;

            return await ExecuteAsync<IRequest<TResponse>, TResponse>(
                cmd, result => Created(string.Empty, new ApiResponse<TResponse>(result, "Created successfully.")));
        }


        [HttpPut]
        protected virtual async Task<IActionResult> UpdateAsync<TEntity, TResponse>(IMapWith<TEntity> command)
            where TEntity : class, IEntityBase
        {
            var obj = Mapper.Map<TEntity>(command);
            var cmd = new UpdateEntityCommand<TEntity>(obj) as IRequest<TResponse>;

            return await ExecuteAsync<IRequest<TResponse>, TResponse>(cmd, result => Ok(new ApiResponse<TResponse>(result, "Updated successfully.")));
        }


        [HttpDelete("{id}")]
        protected virtual async Task<IActionResult> DeleteAsync<TCommand>(TCommand command)
            where TCommand : IRequest<Unit>
        {
            try
            {
                await Mediator.Send(command);
                return Ok(new ApiResponse("Deleted successfully."));
            }
            catch (ValidationException ex)
            {
                return BadRequest(new ApiResponse(
                    ex.Errors,
                    "Validation failed."
                ));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ApiResponse(
                    new List<string> { ex.Message },
                    "Internal server error."
                ));
            }
        }
    }
}
