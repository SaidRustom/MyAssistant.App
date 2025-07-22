using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Domain.Base
{
    /// <summary>
    /// Represents a log record for actions performed by the MyAssistant service.
    /// Captures important details such as the service type, timestamps for operation start and end, and any result description.
    /// </summary>
    [Table("MyAssistantServiceLog")]
    public class MyAssistantServiceLog
    {
        [Required]
        public Guid Id { get; set; }

        public string? ResultDescription { get; set; }

        [Required]
        public int MyAssistantServiceTypeCode { get; set; }

        [Required]
        public MyAssistantServiceType MyAssistantServiceType { get; set; } = default!;

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }
        
        public MyAssistantServiceLog() { }

        public MyAssistantServiceLog(MyAssistantServiceType serviceType)
        {
            Id = Guid.NewGuid();
            MyAssistantServiceTypeCode = serviceType.Code;
        }

    }
}
