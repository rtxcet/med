using med.Models;
using Microsoft.AspNetCore.Mvc;

namespace med.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;

        public ContactController(AppDbContext context)
        {
            _context = context;
        }
       
        [HttpPost]
        public async Task<IActionResult> SubmitForm(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();
                TempData["Success"] = "Mesaj başarıyla gönderildi.";
            }
            else
            {
                TempData["Error"] = "Lütfen tüm alanları doğru doldurun.";
            }

            return Redirect("/Default/Index#contact");

        }
    }
}
