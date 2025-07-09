using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
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

    public class GetEntityByIdQueryHandler<TEntity, TResponse> :
        IGetEntityByIdQueryHandler<TEntity, TResponse>
        where TEntity : class, IEntityBase
        where TResponse : IDto<TEntity>
    {
        private readonly IBaseAsyncRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly Guid _userID; 

        public GetEntityByIdQueryHandler(IBaseAsyncRepository<TEntity> repository, IMapper mapper, ILoggedInUserService userService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userID = userService.UserId;
        }

        public async Task<TResponse> Handle(GetEntityByIdQuery<TEntity, TResponse> request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var entity = await _repository.GetByIdAsync(request.Id);
            if (entity == null)
                throw new KeyNotFoundException($"{typeof(TEntity).Name} with id '{request.Id}' was not found.");

            var entityToReturn = _mapper.Map<TEntity, TResponse>(entity);

            ValidateAccessPermission(entityToReturn); 
            return entityToReturn;
        }

        #region Helper Methods

        /// <summary>
        /// Throw exception if user cannot access the entity & Include PermissionType for Shareable type entities
        /// This is called after db retrival to include ShareWith users
        /// Could be improved by separating into 2 methods (one for permission & one for access validation using fluent)
        /// </summary>
        void ValidateAccessPermission(TResponse dto)
        {
            var shareableDtoInterface = dto
                .GetType()
                .GetInterfaces()
                .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IShareableDto<>));

            if (shareableDtoInterface == null) //Not a shareable type
            {
                if (dto.UserId.Equals(_userID))
                    return;
                else
                    throw new UnauthorizedAccessException($"Unauthorized access");
            }

            PermissionType permission = new();

            if (dto.UserId.Equals(_userID)) //Current user is owner of the object
            {
                permission = PermissionTypeList.Get(PermissionType.ReadWriteDelete);
            }

            else
            {
                ICollection<EntityShare> shares = new List<EntityShare>();
                var prop = shareableDtoInterface.GetProperty("Shares")!.GetValue(shares);

                var share = shares.FirstOrDefault(share => share.SharedWithUserId == _userID)
                    ?? throw new UnauthorizedAccessException("Unauthorized access");

                permission = share.PermissionType;
            }

            //Populate PermissionType in the returned Dto (Interface enforced)
            var permissionProp = shareableDtoInterface.GetProperty("PermissionType");
            permissionProp!.SetValue(dto, permission, null);
        }

        #endregion
    }
}