using MediatR;
using MyAssistant.Domain.Models;
using System.ComponentModel.DataAnnotations;

namespace MyAssistant.Shared.DTOs
{
    public class CreateRecurrenceCommand: IRequest<Guid>, IMapWith<Recurrence>
    {
        [Required]
        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public int Interval { get; set; } 

        [Range(1, 7)]
        public int RecurrenceTypeCode { get; set; }

        [Range(1, 5)]
        public int DefaultPriority { get; set; }

        public TimeSpan? Time { get; set; }
        public int LengthInMinutes { get; set; }
        public DateTime StartDate { get; set; } 
        public DateTime? EndDate { get; set; }
    }
}
