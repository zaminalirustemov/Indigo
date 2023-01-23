using Microsoft.AspNetCore.Identity;

namespace Indigo_Travel.Models;
public class AppUser: IdentityUser
{
    public string FullName { get; set; }
}

