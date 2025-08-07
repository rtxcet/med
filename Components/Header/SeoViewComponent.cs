using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using med.Models;

namespace med.Components
{
    public class SeoViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public SeoViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var site = await _context.Sites.FirstOrDefaultAsync();
            return View(site);
        }
    }
}
