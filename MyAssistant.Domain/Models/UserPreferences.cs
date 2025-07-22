using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MyAssistant.Domain.Base;

namespace MyAssistant.Domain.Models;

[Table("UserPreferences")]
public class UserPreferences : EntityBase
{
    public bool DarkMode { get; set; }

    public bool EmailNotifications {  get; set; }

    public bool EnableChat { get; set; }

    public bool DiscoverableByOtherUsers { get; set; }
}
