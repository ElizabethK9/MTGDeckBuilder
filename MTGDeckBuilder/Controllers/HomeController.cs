using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Models;
using System.Diagnostics;

namespace MTGDeckBuilder.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        [AllowAnonymous] // Anyone can access main page
        public IActionResult Index()
        {
            ViewBag.PageTitle = "MTGDeckBuilder";
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
