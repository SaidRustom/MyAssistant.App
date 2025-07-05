using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using MyAssistant.Identity.Models;
using System.Security.Claims;

namespace MyAssistant.Identity.Services
{
    public class LocalUserProfileService : IProfileService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public LocalUserProfileService(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var subjectId = context.Subject.GetSubjectId();
            var user = await _userManager.FindByNameAsync(subjectId);

            if (user == null)
            {
                return;
            }

            var claimsForUser = (await _userManager
                .GetClaimsAsync(user))
                .ToList();

            context.AddRequestedClaims(
                claimsForUser.Select(c => new Claim(c.Type, c.Value)).ToList());

        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;

            //var subjectId = context.Subject.GetSubjectId();
            //context.IsActive = await _localUserService
            //    .IsUserActive(subjectId);
        }
    }
}
