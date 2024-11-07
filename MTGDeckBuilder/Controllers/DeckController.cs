using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Data;
using MTGDeckBuilder.Models;
using Microsoft.EntityFrameworkCore;
#nullable disable
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
        public async Task<IActionResult> ViewAllDecks() 
        {
            // Get current logged user
            IdentityUser user = await _userManager.GetUserAsync(User);

            // Get all decks made by the current user from the db


            // Send all user's decks into the view

            return View(); 
        }

        [HttpPost]
        public IActionResult ViewAllDecks(GameDeck deck)
        {
            // Create deck logic (check if user clicked the button)
            // Send User to the deck they clicked (if they clicked an existing deck)
            return View();
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
           
            IdentityUser user = await _userManager.GetUserAsync(User);

            // Quite possibly redundant code because DeckController is set to [Authorize]
            if (user == null)
            {
                TempData["ErrorMessage"] = "User not found";
                return View(deck);
            }

            UserInventory currentUsersInventory = await _context.UserInventories
                .FirstOrDefaultAsync(ui => ui.IdentityUserId == user.Id);

            // Quite possibly redundant code because all users should have an inventory,
            // empty or not.
            if (currentUsersInventory == null)
            {
                TempData["ErrorMessage"] = "User inventory not found";
                return View(deck);
            }

            currentUsersInventory.AddDeck(_context, deck);

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Deck created successfully";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while saving the deck";
            }

            return View(deck);
        }

    }
}
