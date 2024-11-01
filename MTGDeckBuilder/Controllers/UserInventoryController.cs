using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Models;

namespace MTGDeckBuilder.Controllers
{
    public class UserInventoryController : Controller
    {
        public IActionResult Create()
        {
            return View(new UserInventory());
        }
    }
}
