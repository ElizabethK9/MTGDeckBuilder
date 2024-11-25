using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Models;

namespace MTGDeckBuilder.Controllers
{
    public class ResourcesController : Controller
    {
        public IActionResult ResourcesView()
        {
            // Pass contact information to the view using ViewBag
            ViewBag.NameOne = Resources.NameOne;
            ViewBag.EmailOne = Resources.EmailOne;
            ViewBag.DiscordIDOne = Resources.DiscordIDOne;

            ViewBag.NameTwo = Resources.NameTwo;
            ViewBag.EmailTwo = Resources.EmailTwo;
            ViewBag.DiscordIDTwo = Resources.DiscordIDTwo;

            return View();
        }
    }
}
