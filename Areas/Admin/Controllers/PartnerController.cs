using med.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace med.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PartnerController : Controller
    {
        private readonly AppDbContext _context;

        public PartnerController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var partners = await _context.Partners.ToListAsync();

            ViewBag.Message = TempData["SuccessMessage"];
            return View(partners);
        }


        public async Task<IActionResult> Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Partner partner)
        {
            if (ModelState.IsValid)
            {
                // Eğer görsel yükleme varsa (opsiyonel olarak işle)
                if (partner.ImageFile != null)
                {
                    var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads/partner");

                    // Klasör yoksa oluştur
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(partner.ImageFile.FileName);
                    var path = Path.Combine(uploadsFolder, fileName);

                    // Dosyayı asenkron şekilde kaydet (await çok önemli!)
                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await partner.ImageFile.CopyToAsync(stream);
                    }

                    partner.ImageUrl = "/uploads/partner/" + fileName;
                }


                _context.Partners.Add(partner);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Başarıyla eklendi.";
                return RedirectToAction("Index");
            }

            return View(partner);
        }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner == null)
                return NotFound();

            // Resmi sil
            if (!string.IsNullOrEmpty(partner.ImageUrl))
            {
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", partner.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(imagePath))
                    System.IO.File.Delete(imagePath);
            }

            _context.Partners.Remove(partner);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Başarıyla silindi.";
            return RedirectToAction("Index");
        }





    }
}
