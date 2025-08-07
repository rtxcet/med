using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using med.Models;

namespace med.Components
{
    public class YoutubeViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public YoutubeViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var link = await _context.Links.FirstOrDefaultAsync();
            return View(link);
        }
    }
}
