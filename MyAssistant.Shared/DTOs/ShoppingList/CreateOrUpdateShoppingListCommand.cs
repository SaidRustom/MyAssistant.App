using System.ComponentModel.DataAnnotations;
using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class CreateOrUpdateShoppingListCommand : IRequest<Guid>, IMapWith<ShoppingList>
{
    public Guid? Id { get; set; }
    
    [Required, StringLength(200)]
    public string Name { get; set; } = string.Empty;

    [StringLength(1000)]
    public string? Description { get; set; }
}