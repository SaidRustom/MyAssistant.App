using System.ComponentModel.DataAnnotations;
using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class CreateOrUpdateTaskItemCommand : IRequest<Guid>, IMapWith<TaskItem>
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(1000)]
        public string? Description { get; set; }

        public DateTime? ScheduledAt { get; set; }

        public DateTime? DueDate { get; set; }

        public int LengthInMinutes { get; set; }

        public bool IsCompleted { get; set; }

        [Range(0, 5)]
        public int Priority { get; set; }

        public Guid? RecurrenceId { get; set; }

        public Guid? GoalId { get; set; }
    }
}
