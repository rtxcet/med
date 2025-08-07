using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using med.Models;

namespace med.Components
{
    public class PartnersViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public PartnersViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var partners = await _context.Partners
                .OrderBy(p => p.Id)
                .ToListAsync();

            return View(partners);
        }
    }
}
