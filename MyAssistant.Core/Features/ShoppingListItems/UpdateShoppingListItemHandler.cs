using AutoMapper;
using FluentValidation;
using MediatR;
using MyAssistant.Core.Contracts;
using MyAssistant.Core.Contracts.Persistence;
using MyAssistant.Core.Features.Notifications.Handle;
using MyAssistant.Core.Validators;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Core.Features.ShoppingListItems;

public class UpdateShoppingListItemHandler : IRequestHandler<CreateOrUpdateShoppingListItemCommand, Guid>
{
    private readonly IBaseAsyncRepository<ShoppingListItem> _repo;
    private readonly IBaseAsyncRepository<ShoppingList> _listRepo;
    private readonly ILoggedInUserService  _loggedInUserService;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public UpdateShoppingListItemHandler(
        IBaseAsyncRepository<ShoppingListItem> repo,
        IBaseAsyncRepository<ShoppingList> listRepo,
        ILoggedInUserService loggedInUserService,
        IMapper mapper,
        IMediator mediator)
    {
        _repo = repo;
        _listRepo = listRepo;
        _mapper = mapper;
        _loggedInUserService = loggedInUserService;
        _mediator = mediator;
    }

    public async Task<Guid> Handle(CreateOrUpdateShoppingListItemCommand cmd, CancellationToken cancellationToken)
    {
        var obj = _mapper.Map<ShoppingListItem>(cmd);

        var dbValue = await _repo.GetByIdAsync(cmd.Id.Value);
        await _repo.DetachAsync(dbValue); //we don't want EF to track this..
        if (dbValue == null)
            throw new KeyNotFoundException();

        var validator = new ShoppingListItemValidator(_repo, _listRepo, _loggedInUserService);
        await validator.ValidateAndThrowAsync(obj, cancellationToken);

        if (dbValue.IsActive != cmd.IsActive && !cmd.IsActive)
        {
            obj.TotalPurchaseCount = dbValue.TotalPurchaseCount + obj.Quantity;
            obj.Quantity = 0;
            obj.LastPurchaseDate = DateTime.Now;

            HandleRecurrence(cmd, obj, dbValue);
        }

        else if (dbValue.IsActive != cmd.IsActive && cmd.IsActive)
        {
            obj.NextOccurrenceDate = null;
        }

        //RecurrenceType changed but the item active status didn't change?
        else if(cmd.RecurrenceTypeCode != dbValue.RecurrenceTypeCode && cmd.IsRecurring)
        {
            if (dbValue == null)
                dbValue = new();
            HandleRecurrence(cmd, obj,dbValue);
        }

        await _repo.UpdateAsync(obj);
        await _mediator.Send(new HandleNotificationsCommand<ShoppingList>(cmd.ShoppingListId));
        
        return cmd.Id.Value;
    }

    public void HandleRecurrence(CreateOrUpdateShoppingListItemCommand cmd, ShoppingListItem obj, ShoppingListItem dbValue)
    {
        if (dbValue.NextOccurrenceDate != cmd.NextOccurrenceDate)
        {   //Insert NextOccurrenceDate if the date wasn't edited by the user..
            if (obj.RecurrenceTypeCode == RecurrenceType.Daily)
                obj.NextOccurrenceDate = DateTime.Now.AddDays(obj.RecurrenceInterval);
            else if (obj.RecurrenceTypeCode == RecurrenceType.Weekly)
                obj.NextOccurrenceDate = DateTime.Now.AddDays(obj.RecurrenceInterval * 7);
            else if (obj.RecurrenceTypeCode == RecurrenceType.Monthly)
                obj.NextOccurrenceDate = DateTime.Now.AddMonths(obj.RecurrenceInterval);
            else if (obj.RecurrenceTypeCode == RecurrenceType.Annually)
                obj.NextOccurrenceDate = DateTime.Now.AddYears(obj.RecurrenceInterval);
        }
    }
}