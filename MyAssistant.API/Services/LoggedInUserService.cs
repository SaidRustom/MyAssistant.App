using MyAssistant.Core.Contracts;
using System.Security.Claims;

namespace MyAssistant.API.Services
{
    /// <summary>
    /// Retrieves the unique identifier (User ID) of the currently logged-in user from the HTTP context's claims.
    /// Assumes that the user's ID is stored in the "sub" (subject) claim.
    /// </summary>
    /// <returns>A <see cref="Guid"/> representing the user's ID.</returns>
    /// <exception cref="ArgumentNullException">
    /// Thrown if the HTTP context or user claim is not available.
    /// </exception>
    /// <exception cref="FormatException">
    /// Thrown if the "sub" claim cannot be parsed as a <see cref="Guid"/>.
    /// </exception>
    public class LoggedInUserService : ILoggedInUserService
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public LoggedInUserService(IHttpContextAccessor httpContextAccessor)
        {
            _contextAccessor = httpContextAccessor;
        }

        public Guid UserId
        {
            get
            {
                return Guid.Parse(_contextAccessor.HttpContext?.User?.FindFirstValue("sub"));
            }
        }
    }
}
