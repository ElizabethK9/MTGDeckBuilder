using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Data;
using MTGDeckBuilder.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Add this for logging

namespace MTGDeckBuilder.Controllers
{
    [Authorize]
    public class DeckController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public DeckController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(GameDeck deck)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Deck is invalid";
                return View(deck);
            }

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                _logger.LogWarning("User not found");
                TempData["ErrorMessage"] = "User not found";
                return View(deck);
            }

            var userInventory = await _context.UserInventories
                .FirstOrDefaultAsync(ui => ui.IdentityUserId == user.Id);

            if (userInventory == null)
            {
                _logger.LogWarning("User inventory not found for user ID {UserId}", user.Id);
                TempData["ErrorMessage"] = "User inventory not found";
                return View(deck);
            }

            userInventory.AddDeck(_context, deck);

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Deck created successfully";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving deck to database");
                TempData["ErrorMessage"] = "An error occurred while saving the deck";
            }

            return View(deck);
        }

    }
}
