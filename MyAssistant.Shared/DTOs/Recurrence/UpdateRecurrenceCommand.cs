using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs;

public class UpdateRecurrenceCommand : CreateRecurrenceCommand, IRequest<Guid>, IMapWith<Recurrence>
{
    public Guid Id { get; set; }  
    
    public bool UpdateAllOccurrences { get; set; }
    
}