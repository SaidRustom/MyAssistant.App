using MediatR;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Base.Create;
using MyAssistant.Core.Features.Base.Get;
using MyAssistant.Core.Features.Base.GetList;
using Models = MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.UserPreferences.GetAndCreateIfEmpty
{
    public class GetUserPreferencesAndCreateIfEmpty : IRequest<UserPreferencesDto>
    {

    }

    public class GetUserPreferencesAndCreateIfEmptyHandler (IMediator mediator, IBaseAsyncRepository<Models.UserPreferences> repo)
        : IRequestHandler<GetUserPreferencesAndCreateIfEmpty,
            UserPreferencesDto>
    {

        public async Task<UserPreferencesDto> Handle(GetUserPreferencesAndCreateIfEmpty query, CancellationToken cancellationToken)
        {
            var preferences = await mediator.Send(new GetEntityListQuery<Models.UserPreferences, UserPreferencesDto>(1,1));

            if (preferences.Items.Any())
                return preferences.Items.First();
            
            else
            {
                var preferencesId = await mediator.Send(new CreateEntityCommand<Models.UserPreferences>(new()));
                var newPreferences = await mediator.Send(new GetEntityByIdQuery<Models.UserPreferences, UserPreferencesDto>(preferencesId));
                return newPreferences;
            }
        }
    }

}
