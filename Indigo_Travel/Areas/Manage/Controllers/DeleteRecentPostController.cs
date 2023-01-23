using Indigo_Travel.Contex;
using Indigo_Travel.Helpers;
using Indigo_Travel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Indigo_Travel.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin , Admin")]

public class DeleteRecentPostController : Controller
{
    private readonly IndigoDbContext _indigoDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public DeleteRecentPostController(IndigoDbContext indigoDbContext,IWebHostEnvironment webHostEnvironment)
    {
        _indigoDbContext = indigoDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //READ----------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<RecentPost> recentPosts = _indigoDbContext.RecentPosts
                                                       .Where(x => x.IsDeleted == true)
                                                       .ToList();
        return View(recentPosts);
    }
    //Restore----------------------------------------------------------------------------------
    public IActionResult Restore(int id)
    {
        RecentPost recentPost = _indigoDbContext.RecentPosts.FirstOrDefault(x => x.Id == id);
        if (recentPost is null) return NotFound();

        recentPost.IsDeleted = false;
        _indigoDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));

    }
    //HardDELETE----------------------------------------------------------------------------------
    public IActionResult HardDelete(int id)
    {
        RecentPost recentPost = _indigoDbContext.RecentPosts.FirstOrDefault(x => x.Id == id);
        if (recentPost is null) return NotFound();


        FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/recentpost", recentPost.ImageName);

        _indigoDbContext.RecentPosts.Remove(recentPost);
        _indigoDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));

    }
}

