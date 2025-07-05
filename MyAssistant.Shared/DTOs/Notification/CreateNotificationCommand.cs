using System.ComponentModel.DataAnnotations;
using MediatR;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class CreateNotificationCommand : IRequest<Guid>, IMapWith<Notification>
    {
        [Required, StringLength(200)]
        public string Title { get; set; } = default!;

        [StringLength(200)]
        public string? Message { get; set; }

        public bool IsRead { get; set; }

        public DateTime? ReadAt { get; set; }

        public string? ActionUrl { get; set; }

        public bool EmailNotification { get; set; }

        public string? ObjectType { get; set; }

        public Guid? ObjectId { get; set; }
    }
}
