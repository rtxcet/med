using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace med.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
