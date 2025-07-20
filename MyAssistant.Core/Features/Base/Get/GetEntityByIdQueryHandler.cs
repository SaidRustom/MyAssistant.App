using System.Collections.ObjectModel;
using System.Security;
using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Notifications.MarkRead;
using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;
using MyAssistant.Shared;

namespace MyAssistant.Core.Features.Base.Get
{
    public interface IGetEntityByIdQueryHandler<TEntity, TResponse>
        : IRequestHandler<GetEntityByIdQuery<TEntity, TResponse>, TResponse>
        where TEntity : class, IEntityBase
        where TResponse : IDto<TEntity>
    { }

    /// <summary>
    /// Handles the retrieval of a specific <typeparamref name="TEntity"/> by its identifier.
    /// Throws a <see cref="KeyNotFoundException"/> if the entity does not exist. 
    /// The method also maps the entity to the result DTO type <typeparamref name="TResponse"/> 
    /// and verifies access permissions before returning the result.
    /// </summary>
    public class GetEntityByIdQueryHandler<TEntity, TResponse> :
        IGetEntityByIdQueryHandler<TEntity, TResponse>
        where TEntity : class, IEntityBase
        where TResponse : IDto<TEntity>
    {
        private readonly IBaseAsyncRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly Guid _userID;
        private readonly IMediator _mediator;

        public GetEntityByIdQueryHandler(IBaseAsyncRepository<TEntity> repository, IMapper mapper, ILoggedInUserService userService, IMediator mediator)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userID = userService.UserId;
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        public async Task<TResponse> Handle(GetEntityByIdQuery<TEntity, TResponse> request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            await _repository.ValidateCanGetAsync(request.Id, _userID);

            var entity = await _repository.GetByIdAsync(request.Id);

            var entityToReturn = _mapper.Map<TEntity, TResponse>(entity);

            AddPermissionTypeToShareableDtos(entityToReturn);
            await _mediator.Send(new MarkEntityNotificationsReadCommand(entityToReturn.Id));

            return entityToReturn;
        }

        #region Helper Methods

        /// <summary>
        /// Throw exception if user cannot access the entity & Include PermissionType for Shareable type entities
        /// This is called after db retrival to include ShareWith users
        /// Could be improved by separating into 2 methods (one for permission & one for access validation using fluent)
        /// </summary>
        async void AddPermissionTypeToShareableDtos(TResponse dto)
        {
            var isShareableDtoInterface = dto
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IShareableDto<>));

            if(isShareableDtoInterface != null)
            {
                var permission = await _repository.GetUserPermissionTypeAsync(dto.Id, _userID);

                //Populate PermissionType in the returned Dto (Interface enforced)
                var permissionProp = isShareableDtoInterface.GetProperty("PermissionType");
                permissionProp!.SetValue(dto, permission, null);
            }
        }

        #endregion
    }
}