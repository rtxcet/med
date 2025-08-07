using med;
using med.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class YoutubeController : Controller
{
    private readonly AppDbContext _context;

    public YoutubeController(AppDbContext context)
    {
        _context = context;
    }

    // GET: /Link
    public async Task<IActionResult> Index()
    {
        var link = await _context.Links.FirstOrDefaultAsync();

        if (link == null)
        {
            link = new Link { Src = "https://example.com" }; // Veya boş bırak
            _context.Links.Add(link);
            await _context.SaveChangesAsync();
        }

        // TempData'dan gelen mesajı al ve ViewBag'e ata
        ViewBag.Message = TempData["SuccessMessage"];

        return View(link);
    }


    // POST: /Link
    [HttpPost]
    public async Task<IActionResult> Index(Link model)
    {
        var existingLink = await _context.Links.FirstOrDefaultAsync();

        if (existingLink != null)
        {
            existingLink.Src = model.Src;
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Başarıyla güncellendi.";
        }

        return RedirectToAction("Index");
    }

}
