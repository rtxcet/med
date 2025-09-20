using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using med.Models;

namespace med.Components
{
    public class CategoryViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public CategoryViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Son eklenen 9 kategori (Id'ye göre sıralı)
            var categories = await _context.Categories
                .OrderByDescending(c => c.Id)
                .Take(9)
                .ToListAsync();

            return View(categories);
        }
    }
}
