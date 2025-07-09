using MyAssistant.Domain.Base;
using MyAssistant.Domain.Lookups;
using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class RecurrenceDto : CreateRecurrenceCommand ,IDto<Recurrence>
    {
        public new Guid Id { get; set; }

        public Guid UserId { get; set; }

        public RecurrenceType RecurrenceType { get; set; } = default!;

        public ICollection<AuditLog> AuditLogs { get; set; } = new List<AuditLog>();

    }
}
