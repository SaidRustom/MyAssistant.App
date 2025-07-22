using MediatR;
using Microsoft.AspNetCore.Mvc;
using MyAssistant.Core.Responses;
using MyAssistant.Core.Exceptions;
using AutoMapper;
using MyAssistant.Shared;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Update;
using MyAssistant.Core.Features.Base.Get;
using MyAssistant.Core.Features.Base.GetList;
using MyAssistant.Persistence.Repositories.Base;
using MyAssistant.Core.Contracts.Persistence;

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

        /// <summary>
        /// Executes the given <paramref name="request"/> using the Mediator and handles the result using the provided <paramref name="onSuccess"/> delegate.
        /// Centralizes exception handling for common scenarios such as validation errors and unexpected server exceptions,
        /// returning standardized API responses for each case.
        /// </summary>
        /// <typeparam name="TRequest">
        ///     The type of the request to be processed, which must implement <see cref="IRequest{TResponse}"/>.
        /// </typeparam>
        /// <typeparam name="TResponse">
        ///     The type of the response returned by the request and the delegate.
        /// </typeparam>
        /// <param name="request">The request command or query to be sent via Mediator.</param>
        /// <param name="onSuccess">
        ///     A delegate that constructs and returns an <see cref="IActionResult"/> from a successful <typeparamref name="TResponse"/>.
        /// </param>
        /// <returns>
        ///     An <see cref="IActionResult"/> representing:
        ///         - the successful result (<paramref name="onSuccess"/> with response),
        ///         - a 400 Bad Request with validation errors,
        ///         - or a 500 Internal Server Error for unhandled exceptions.
        /// </returns>
        protected async Task<IActionResult> ExecuteAsync<TRequest, TResponse>(
            TRequest request,
            Func<TResponse, IActionResult> onSuccess) where TRequest : IRequest<TResponse>
        {
            try
            {
                var response = await Mediator.Send(request);
                return onSuccess(response);
            }
            catch (ValidationException validation) //TODO: Log the exceptions
            {
                return BadRequest(new ApiResponse<TResponse>(
                    validation.Errors,
                    "Validation failed."
                ));
            }
            catch (UnauthorizedAccessException unauthorized)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, new ApiResponse<TResponse>(
                    new List<string> { unauthorized.Message },
                    "Unauthorized."
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

        /// <summary>
        /// Retrieves an entity of type <typeparamref name="TEntity"/> by its identifier,
        /// executes the associated query, and returns a formatted API response wrapping the result
        /// mapped to <typeparamref name="TResponse"/>.
        /// </summary>
        /// <typeparam name="TEntity">The entity type to retrieve. Must implement <see cref="IEntityBase"/>.</typeparam>
        /// <typeparam name="TResponse">The response DTO type. Must implement <see cref="IDto{TEntity}"/>.</typeparam>
        /// <returns>
        /// An <see cref="IActionResult"/> containing an <see cref="ApiResponse{TResponse}"/> if found,
        /// or an appropriate HTTP status code otherwise.
        /// </returns>
        [HttpGet("{id}")]
        protected virtual async Task<IActionResult> GetAsync<TEntity, TResponse>(Guid id)
            where TEntity : class, IEntityBase
            where TResponse : IDto<TEntity>
        {
            var query = new GetEntityByIdQuery<TEntity, TResponse>(id);

            return await ExecuteAsync<IRequest<TResponse>, TResponse>(
                query, result => Ok(new ApiResponse<TResponse>(result)));
        }
        
        [HttpGet]
        public async Task<IActionResult> GetListAsync<TEntity, TResponse>(int pageNumber = 1, int pageSize = 20)
            where TEntity : class, IEntityBase
            where TResponse : IDto<TEntity>
        {
            var query = new GetEntityListQuery<TEntity, TResponse>(pageNumber, pageSize);

            return await ExecuteAsync<GetEntityListQuery<TEntity, TResponse>, PaginatedList<TResponse>>(
                query,
                result => Ok(new ApiResponse<PaginatedList<TResponse>>(result))
            );
        }

        /// <summary>
        /// Handles HTTP POST requests for creating a new entity of type <typeparamref name="TEntity"/>.
        /// Maps the incoming <paramref name="command"/> to an entity object, wraps it in a creation command,
        /// and executes it. Returns a 201 Created response upon successful creation, 
        /// along with the response payload wrapped in an <see cref="ApiResponse{TResponse}"/>.
        /// </summary>
        /// <typeparam name="TEntity">The type of entity being created.</typeparam>
        /// <typeparam name="TResponse">The type of response returned after creation.</typeparam>
        /// <param name="command">The input command object mapped to <typeparamref name="TEntity"/>.</param>
        /// <returns>An <see cref="IActionResult"/> including the UID of the created entity.</returns>
        [HttpPost]
        protected virtual async Task<IActionResult> CreateAsync<TEntity, TResponse>(IMapWith<TEntity> command)
            where TEntity : class, IEntityBase
        {
            var obj = Mapper.Map<TEntity>(command);
            var cmd = new CreateEntityCommand<TEntity>(obj) as IRequest<TResponse>;

            return await ExecuteAsync<IRequest<TResponse>, TResponse>(
                cmd, result => Created(string.Empty, new ApiResponse<TResponse>(result, "Created successfully.")));
        }

        /// <summary>
        /// Handles the HTTP PUT request to update an existing entity.
        /// Maps the input command to the entity type, wraps it in an update command,
        /// and executes the update operation asynchronously. Returns a standardized API response upon success.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity to be updated.</typeparam>
        /// <typeparam name="TResponse">The type of the response returned to the client.</typeparam>
        /// <param name="command">The command object containing updated values for the entity.</param>
        /// <returns>An <see cref="IActionResult"/> containing the API response for the update operation.</returns>
        [HttpPut]
        protected virtual async Task<IActionResult> UpdateAsync<TEntity, TResponse>(IMapWith<TEntity> command)
            where TEntity : class, IEntityBase
        {
            var obj = Mapper.Map<TEntity>(command);
            var cmd = new UpdateEntityCommand<TEntity>(obj) as IRequest<TResponse>;

            return await ExecuteAsync<IRequest<TResponse>, TResponse>(cmd, result => Ok(new ApiResponse<TResponse>(result, "Updated successfully.")));
        }

        /// <summary>
        /// Handles HTTP DELETE requests by executing the specified command using the Mediator pattern.
        /// </summary>
        /// <typeparam name="TCommand">
        ///     A command type that implements <see cref="IRequest{Unit}"/> and represents the delete operation.
        /// </typeparam>
        /// <param name="command">The command object containing parameters required to perform the delete action.</param>
        /// <returns>
        ///     An <see cref="IActionResult"/> representing the outcome of the operation.
        ///     - 200 OK with a success message if the command is handled successfully.
        ///     - 400 Bad Request with validation errors if validation fails.
        ///     - 500 Internal Server Error with an error message for unhandled exceptions.
        /// </returns>
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
