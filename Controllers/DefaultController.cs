using Microsoft.AspNetCore.Mvc;

namespace med.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
