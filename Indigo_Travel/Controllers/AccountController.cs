using Indigo_Travel.Contex;
using Indigo_Travel.Models;
using Indigo_Travel.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks.Sources;

namespace Indigo_Travel.Controllers;
public class AccountController : Controller
{
    private readonly IndigoDbContext _indigoDbContext;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(IndigoDbContext indigoDbContext,UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _indigoDbContext = indigoDbContext;
        _userManager = userManager;
        _signInManager = signInManager;
    }
    //REGISTER----------------------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Register()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Register(MemberRegisterViewModel memberRegisterVM)
    {
        if (!ModelState.IsValid) return View();

        AppUser user = null;

        user = _indigoDbContext.Users.FirstOrDefault(x => x.NormalizedUserName == memberRegisterVM.Username.ToUpper());
        if (user is not null)
        {
            ModelState.AddModelError("Username", "Already exist");
            return View();
        }
        user = _indigoDbContext.Users.FirstOrDefault(x => x.NormalizedEmail == memberRegisterVM.Email.ToUpper());
        if (user is not null)
        {
            ModelState.AddModelError("Email", "Already exist");
            return View();
        }

        user = new AppUser
        {
            FullName = memberRegisterVM.Fullname,
            UserName = memberRegisterVM.Username,
            Email = memberRegisterVM.Email
        };

        var result = await _userManager.CreateAsync(user, memberRegisterVM.Password);

        if (!result.Succeeded)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
        await _userManager.AddToRoleAsync(user, "Member");
        await _signInManager.SignInAsync(user, isPersistent: false);

        return RedirectToAction("index", "home");
    }
    //LogIN & LogOUT-----------------------------------------------------------------------------------------------------------------------------------
    public IActionResult Login()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(MemberLoginViewModel memberLoginVM)
    {
        if (!ModelState.IsValid) return View();
        AppUser user = await _userManager.FindByNameAsync(memberLoginVM.UserName);
        if (user is null)
        {
            ModelState.AddModelError("", "Username or password is false");
            return View();
        }

        var result = await _signInManager.PasswordSignInAsync(user, memberLoginVM.Password, false, false);

        if (!result.Succeeded)
        {
            ModelState.AddModelError("", "Username or password is false");
            return View();
        }

        return RedirectToAction("index", "home");
    }

    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();

        return RedirectToAction("login", "account");
    }
}

