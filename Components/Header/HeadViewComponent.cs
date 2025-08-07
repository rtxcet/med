using med;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HeadViewComponent : ViewComponent
{
    private readonly AppDbContext _context;

    public HeadViewComponent(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var siteSettings = await _context.Sites.FirstOrDefaultAsync();
        return View(siteSettings);
    }
}
