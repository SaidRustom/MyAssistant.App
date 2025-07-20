using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Interfaces
{
    /// <summary>
    /// Interface for shareable entities.
    /// </summary>
    public interface IShareable<T> : IEntityBase where T : IEntityBase
    {
        ICollection<EntityShare>? Shares { get; set; }
        
        bool NotifyOwnerOnChange { get; set; }
        
        string Title { get; set; }
    }
}
