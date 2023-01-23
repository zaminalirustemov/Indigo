using Indigo_Travel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Indigo_Travel.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin , Admin")]
public class DashboardController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public DashboardController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }
    public IActionResult Index()
    {
        return View();
    }
    //////CreateAdmin------------------------------------------------

    //public async Task<IActionResult> CreateAdmin()
    //{
    //    AppUser admin = new AppUser
    //    {
    //        UserName = "Zaminali",
    //        FullName = "Zaminali Rustemov"
    //    };

    //    var result = await _userManager.CreateAsync(admin,"Zamin123");

    //    return Ok(result);

    //}


    //////CreateRole------------------------------------------------

    //public async Task<IActionResult> CreateRole()
    //{
    //    IdentityRole role1 = new IdentityRole("SuperAdmin");
    //    IdentityRole role2 = new IdentityRole("Admin");
    //    IdentityRole role3 = new IdentityRole("Member");

    //    await _roleManager.CreateAsync(role1);
    //    await _roleManager.CreateAsync(role2);
    //    await _roleManager.CreateAsync(role3);

    //    return Ok("Rollar yaradildi");
    //}


    //////AddRole------------------------------------------------
    //public async Task<IActionResult> AddRole()
    //{
    //    AppUser appUser = await _userManager.FindByNameAsync("Zaminali");
    //    await _userManager.AddToRoleAsync(appUser, "SuperAdmin");

    //    return Ok("Rollar menimsedildi");
    //}
}

