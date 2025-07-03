using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Models
{
    [Table("UserConnection")]
    public class UserConnection : AuditableEntity
    {
        [Required]
        public string FriendUserId { get; set; } = default!;

        public RequestStatusType RequestStatus { get; set; }

        public DateTime? RequestSentDate { get; set; } = DateTime.Now;

        [StringLength(250)]
        public string? StatusMessage { get; set; }

        public enum RequestStatusType
        {
            Accepted,
            Declined,
            Pending
        }
    }
}
