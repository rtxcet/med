using Microsoft.AspNetCore.Mvc;
using med.Models;

namespace med.Components
{
    public class ContactMapViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            var model = new Contact(); // Boş form modeli
            return View(model);        // Form sayfasına bu modeli gönderiyoruz
        }
    }
}
