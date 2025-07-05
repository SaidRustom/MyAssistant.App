using MyAssistant.Core.Contracts;
using System.Security.Claims;

namespace MyAssistant.API.Services
{
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
