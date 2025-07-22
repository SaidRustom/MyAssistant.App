using MyAssistant.Domain.Models;

namespace MyAssistant.Shared.DTOs
{
    public class UserPreferencesDto : IDto<UserPreferences>, IMapWith<UserPreferences>
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public bool DarkMode { get; set; }

        public bool EmailNotifications { get; set; }

        public bool EnableChat { get; set; }

        public bool DiscoverableByOtherUsers { get; set; }
    }
}
