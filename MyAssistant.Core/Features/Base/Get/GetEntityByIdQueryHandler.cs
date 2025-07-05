using AutoMapper;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Domain.Interfaces;
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
            return entityToReturn;
        }
    }
}