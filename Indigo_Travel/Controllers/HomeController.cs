using Indigo_Travel.Contex;
using Indigo_Travel.Models;
using Indigo_Travel.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Indigo_Travel.Controllers;
public class HomeController : Controller
{
    private readonly IndigoDbContext _indigoDbContext;

    public HomeController(IndigoDbContext indigoDbContext)
    {
        _indigoDbContext = indigoDbContext;
    }
    public IActionResult Index()
    {
        HomeViewModel homeViewModel = new HomeViewModel
        {
            RecentPosts = _indigoDbContext.RecentPosts
                                          .Where(x => x.IsDeleted == false)
                                          .ToList()
        };
        return View(homeViewModel);

    }
}


