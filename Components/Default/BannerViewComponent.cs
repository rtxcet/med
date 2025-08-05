using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using med.Models;

namespace med.Components
{
    public class BannerViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public BannerViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var sliders = await _context.Sliders.OrderBy(s => s.Id).ToListAsync();
            return View(sliders);
        }
    }
}
