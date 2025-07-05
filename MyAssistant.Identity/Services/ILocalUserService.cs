using MyAssistant.Identity.Models;

namespace MyAssistant.Identity.Services

{
    public interface ILocalUserService
    {
        Task<ApplicationUser> GetUserByEmailAsync(string email);

        void AddUser 
            (ApplicationUser userToAdd, 
            string password);

        Task<bool> IsUserActive(
            string subject);

        Task<bool> ActivateUserAsync(string securityCode);
    }
}
