using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace med.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        private readonly AppDbContext _context;

        public DefaultController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var categoryCount = await _context.Categories.CountAsync();
            var productCount = await _context.Products.CountAsync();

            ViewBag.CategoryCount = categoryCount;
            ViewBag.ProductCount = productCount;

            return View();
        }
    }
}
