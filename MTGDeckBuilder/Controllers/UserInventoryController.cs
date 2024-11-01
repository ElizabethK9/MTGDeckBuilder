using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Models;

namespace MTGDeckBuilder.Controllers
{
    public class UserInventoryController : Controller
    {
        // You have to be logged in to access
        [Authorize]
        public IActionResult Create()
        {
            return View(new UserInventory());
        }
    }
}
