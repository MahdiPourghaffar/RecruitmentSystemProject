using Application.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Infrastracture
{
    public class UserAccesor : IUserAccessor
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserAccesor(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string UserId
        {
            get
            {
                var value = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);

                return value;
            }
        }

        public ClaimsPrincipal GetUserPrinciple
        {
            get
            {
                var user = _httpContextAccessor.HttpContext.User;
                return user;
            }

        }
    }
}
