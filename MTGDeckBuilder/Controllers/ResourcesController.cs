using Microsoft.AspNetCore.Mvc;

namespace MTGDeckBuilder.Controllers
{
    public class ResourcesController : Controller
    {
        public IActionResult Resources()
        {
            return View();
        }
    }
}
