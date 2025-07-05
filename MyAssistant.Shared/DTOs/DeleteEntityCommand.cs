using System.ComponentModel.DataAnnotations;
using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Shared.DTOs
{
    public class DeleteEntityCommand<T> where T : IEntityBase
    {
        [Required]
        public Guid Id { get; set; }
    }
}
