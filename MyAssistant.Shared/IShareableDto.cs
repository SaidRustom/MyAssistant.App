using MyAssistant.Domain.Base;
using MyAssistant.Domain.Interfaces;
using MyAssistant.Domain.Lookups;
using MyAssistant.Shared.DTOs;

namespace MyAssistant.Shared
{
    public interface IShareableDto<T> where T : IShareable<T>
    {
        LookupDto PermissionType { get; set; }

        ICollection<EntityShare> Shares { get; set; }
    }
}
