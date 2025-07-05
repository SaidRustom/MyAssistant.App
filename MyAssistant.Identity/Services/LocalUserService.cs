using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyAssistant.Identity.Data;
using MyAssistant.Identity.Models;
using System.Security.Claims;
using System.Security.Cryptography;

namespace MyAssistant.Identity.Services
{
    public class LocalUserService : ILocalUserService
    {
        private readonly MyAssistantIdentityDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public LocalUserService(
            MyAssistantIdentityDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context ?? 
                throw new ArgumentNullException(nameof(context));

            _userManager = userManager ??
                throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            if (email is null)
            {
                throw new ArgumentNullException(nameof(email));
            }

            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsUserActive(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return false;
            }

            return user.IsActive;
        }

        public void AddUser(ApplicationUser userToAdd, string password)
        {
            if (userToAdd == null) 
            {
                throw new ArgumentNullException(nameof(userToAdd));
            }

            if (_context.Users.Any(u => u.UserName == userToAdd.UserName))
            {
                // in a real-life scenario you'll probably want to 
                // return this as a validation issue
                throw new Exception("Username must be unique");
            }

            if (_context.Users.Any(u => u.Email == userToAdd.Email))
            {
                throw new Exception("Email must be unique");
            }

            userToAdd.SecurityCode = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(128));
            userToAdd.SecurityCodeExpirationDate = DateTime.UtcNow.AddHours(1);

            _userManager.CreateAsync(userToAdd, password);
        }

        public async Task<bool> ActivateUserAsync(string securityCode)
        {
            if (string.IsNullOrWhiteSpace(securityCode))
            {
                throw new ArgumentNullException(nameof(securityCode));
            }

            // find an user with this security code as an active security code.  
            var user = await _context.Users.FirstOrDefaultAsync(u =>
                u.SecurityCode == securityCode &&
                u.SecurityCodeExpirationDate >= DateTime.UtcNow);

            if (user == null)
            {
                return false;
            }

            user.IsActive = true;
            user.SecurityCode = null;
            return true;
        }
    }
}
