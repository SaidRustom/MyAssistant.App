
namespace MyAssistant.Domain.Interfaces
{
    public interface IEntityBase
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
