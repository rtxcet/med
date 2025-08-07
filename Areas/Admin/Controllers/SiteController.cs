using med.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace med.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class SiteController : Controller
    {
        private readonly AppDbContext _context;

        public SiteController(AppDbContext context)
        {
            _context = context;
        }

        // GET
        public async Task<IActionResult> Index()
        {
            var site = await _context.Sites.FirstOrDefaultAsync();

            if (site == null)
            {
                site = new Site
                {
                    Name = "",
                    Description = "",
                    Phone = "",
                    MobilePhone = "",
                    WpPhone = "",
                    Email = "",
                    Adress = "",
                    GoogleMaps = "",
                    SiteMetaKeyword = "",
                    SiteMetaDescription = "",
                    SiteMailAdres = "",
                    SiteMailHost = "",
                    GoogleA = "",
                    GoogleT = ""
                };

                _context.Sites.Add(site);
                await _context.SaveChangesAsync();
            }

            ViewBag.Message = TempData["SuccessMessage"];
            return View(site);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Index(Site model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var site = await _context.Sites.FirstOrDefaultAsync();

            if (site == null)
            {
                _context.Sites.Add(model);
            }
            else
            {
                site.Name = model.Name;
                site.Description = model.Description;
                site.Phone = model.Phone;
                site.MobilePhone = model.MobilePhone;
                site.WpPhone = model.WpPhone;
                site.Email = model.Email;
                site.Adress = model.Adress;
                site.GoogleMaps = model.GoogleMaps;
                site.SiteMetaKeyword = model.SiteMetaKeyword;
                site.SiteMetaDescription = model.SiteMetaDescription;
                site.SiteMailAdres = model.SiteMailAdres;
                site.SiteMailHost = model.SiteMailHost;
                site.GoogleA = model.GoogleA;
                site.GoogleT = model.GoogleT;

                _context.Sites.Update(site);
            }

            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Site bilgileri başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
    }
}
