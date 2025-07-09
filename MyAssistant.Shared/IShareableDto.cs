using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;

namespace MyAssistant.Shared
{
    public interface IShareableDto<T> where T : IShareable<T>
    {
        PermissionType PermissionType { get; set; }

        ICollection<EntityShare> Shares { get; set; }
    }
}
