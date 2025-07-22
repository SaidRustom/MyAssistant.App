using System.ComponentModel.DataAnnotations;
using MediatR;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class CreateOrUpdateShoppingListItemCommand : IRequest<Guid>, IMapWith<ShoppingListItem>
{
    public Guid? Id { get; set; }
    
    [Required, StringLength(200)]
    public string Name { get; set; } = default!;

    public int Quantity { get; set; } = 1;

    public bool IsActive { get; set; } = true;

    [Range(0, 99999)]
    public decimal? UnitPrice { get; set; }

    public bool IsRecurring { get; set; }
    public int RecurrenceTypeCode { get; set; } = RecurrenceType.None;
    public DateTime? NextOccurrenceDate { get; set; }

    [Range(0, 30)]
    public int RecurrenceInterval { get; set; } 

    [Required]
    public Guid ShoppingListId { get; set; }
}