using MyAssistant.Domain.Interfaces;

namespace MyAssistant.Shared
{
    public interface IDto<T> where T : IEntityBase
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
    }
}
