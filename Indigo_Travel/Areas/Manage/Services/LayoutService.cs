using Indigo_Travel.Models;
using Microsoft.AspNetCore.Identity;

namespace Indigo_Travel.Areas.Manage.Services;
public class LayoutService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<AppUser> _userManager;

    public LayoutService(IHttpContextAccessor httpContextAccessor,UserManager<AppUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }
    public async Task<AppUser> GetUser()
    {
        string name = _httpContextAccessor.HttpContext.User.Identity.Name;
        AppUser appUser = await _userManager.FindByNameAsync(name);

        return appUser;
    }
}

