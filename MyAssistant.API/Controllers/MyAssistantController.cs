using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Core.Exceptions;

namespace MyAssistant.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class MyAssistantBaseController : ControllerBase
    {
        protected readonly IMediator Mediator;

        protected MyAssistantBaseController(IMediator mediator)
            => Mediator = mediator;

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
        protected virtual Task<IActionResult> GetAsync<TQuery, TResponse>(TQuery query)
            where TQuery : IRequest<TResponse>
            => ExecuteAsync<TQuery, TResponse>(
                query, result => Ok(new ApiResponse<TResponse>(result))
            );

        [HttpPost]
        protected virtual Task<IActionResult> CreateAsync<TCommand, TResponse>(TCommand command, string routeToAction, object routeValues)
            where TCommand : IRequest<TResponse>
            => ExecuteAsync<TCommand, TResponse>(
                command, result => CreatedAtAction(routeToAction, routeValues, new ApiResponse<TResponse>(result, "Created successfully."))
            );

        [HttpPut]
        protected virtual Task<IActionResult> UpdateAsync<TCommand, TResponse>(TCommand command)
            where TCommand : IRequest<TResponse>
            => ExecuteAsync<TCommand, TResponse>(
                command, result => Ok(new ApiResponse<TResponse>(result, "Updated successfully."))
            );

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
