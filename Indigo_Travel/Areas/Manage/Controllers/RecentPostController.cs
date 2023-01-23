using Indigo_Travel.Contex;
using Indigo_Travel.Helpers;
using Indigo_Travel.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.IO;

namespace Indigo_Travel.Areas.Manage.Controllers;
[Area("Manage")]
[Authorize(Roles = "SuperAdmin , Admin")]
public class RecentPostController : Controller
{
    private readonly IndigoDbContext _indigoDbContext;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public RecentPostController(IndigoDbContext indigoDbContext, IWebHostEnvironment webHostEnvironment)
    {
        _indigoDbContext = indigoDbContext;
        _webHostEnvironment = webHostEnvironment;
    }
    //READ----------------------------------------------------------------------------------
    public IActionResult Index()
    {
        List<RecentPost> recentPosts = _indigoDbContext.RecentPosts
                                                       .Where(x => x.IsDeleted == false)
                                                       .ToList();
        return View(recentPosts);
    }
    //CREATE----------------------------------------------------------------------------------
    public IActionResult Create()
    {
        return View();
    }
    [HttpPost]
    public IActionResult Create(RecentPost recentPost)
    {
        if (recentPost.ImageFile is not null)
        {
            if (recentPost.ImageFile.ContentType != "image/jpeg" && recentPost.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "You can only download images in jpeg and png format");
                return View();
            }
            if (recentPost.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You can only upload images smaller than 2 MB in size");
                return View();
            }

            string filename = FileManager.SaveFile(_webHostEnvironment.WebRootPath, "uploads/recentpost", recentPost.ImageFile);
            recentPost.ImageName = filename;
        }
        else
        {
            return View();
        }

        if (!ModelState.IsValid) return NotFound();

        _indigoDbContext.RecentPosts.Add(recentPost);
        _indigoDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));
    }
    //UPDATE----------------------------------------------------------------------------------
    public IActionResult Update(int id)
    {
        RecentPost recentPost = _indigoDbContext.RecentPosts.FirstOrDefault(x => x.Id == id);
        if (recentPost is null) return NotFound();

        return View(recentPost);
    }
    [HttpPost]
    public IActionResult Update(RecentPost newRecentPost)
    {
        RecentPost existRecentPost = _indigoDbContext.RecentPosts.FirstOrDefault(x => x.Id == newRecentPost.Id);
        if (newRecentPost.ImageFile is not null)
        {
            if (existRecentPost is null) return NotFound();

            if (newRecentPost.ImageFile.ContentType != "image/jpeg" && newRecentPost.ImageFile.ContentType != "image/png")
            {
                ModelState.AddModelError("ImageFile", "You can only download images in jpeg and png format");
                return View();
            }
            if (newRecentPost.ImageFile.Length > 2097152)
            {
                ModelState.AddModelError("ImageFile", "You can only upload images smaller than 2 MB in size");
                return View();
            }
            FileManager.DeleteFile(_webHostEnvironment.WebRootPath, "uploads/recentpost", existRecentPost.ImageName);
            string filename= FileManager.SaveFile(_webHostEnvironment.WebRootPath, "uploads/recentpost", newRecentPost.ImageFile);

            existRecentPost.ImageName = filename;
        }

        existRecentPost.ImageName = existRecentPost.ImageName;
        existRecentPost.Title = newRecentPost.Title;
        existRecentPost.Description = newRecentPost.Description;
        existRecentPost.RedirectUrl = newRecentPost.RedirectUrl;


        if (!ModelState.IsValid) return View();
        _indigoDbContext.SaveChanges();

        return RedirectToAction(nameof(Index));
    }
    //SoftDELETE----------------------------------------------------------------------------------
    public IActionResult SoftDelete(int id)
    {
        RecentPost recentPost = _indigoDbContext.RecentPosts.FirstOrDefault(x => x.Id == id);
        if (recentPost is null) return NotFound();

        recentPost.IsDeleted = true;
        _indigoDbContext.SaveChanges();
        return RedirectToAction(nameof(Index));

    }




}

