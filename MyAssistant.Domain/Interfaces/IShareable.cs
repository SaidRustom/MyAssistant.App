using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Interfaces
{
    /// <summary>
    /// Interface for shareable entities.
    /// </summary>
    public interface IShareable
    {
        ICollection<EntityShare> Shares { get; set; }
    }
}
