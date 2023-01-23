using Indigo_Travel.Models;
using Microsoft.AspNetCore.Identity;

namespace Indigo_Travel.Services
{
    public class MemberLayoutService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MemberLayoutService(UserManager<AppUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<AppUser> GetUser()
        {
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                string name = _httpContextAccessor.HttpContext.User.Identity.Name;

                AppUser appUser = await _userManager.FindByNameAsync(name);
                return appUser;
            }


            return null;
        }
    }
}
