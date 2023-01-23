using Indigo_Travel.Areas.Manage.ViewModles;
using Indigo_Travel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Indigo_Travel.Areas.Manage.Controllers;
[Area("Manage")]
public class AccountController : Controller
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(UserManager<AppUser> userManager,SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(AdminLoginViewModel adminLoginVM)
    {
        if (!ModelState.IsValid) return View();
        AppUser admin = await _userManager.FindByNameAsync(adminLoginVM.UserName);
        if (admin is null)
        {
            ModelState.AddModelError("", "Username or password is false");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(admin, adminLoginVM.Password, false, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or password is false");
            return View();
        }

        return RedirectToAction("index", "dashboard");
    }
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("login", "account");
    }
}

