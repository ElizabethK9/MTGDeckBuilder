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

        public async Task<IActionResult> ViewAllDecks() 
        {
            // Get current logged user
            IdentityUser user = await _userManager.GetUserAsync(User);

            // Get all decks made by the current user from the db
            List<GameDeck> allDecks = await (from GameDeck in _context.GameDecks
                                      where GameDeck.Inventory.IdentityUserId == user.Id
                                      select GameDeck).ToListAsync();

            // Send all user's decks into the view
            return View(allDecks); 
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

            // Query the UserInventory for the logged-in user using query syntax
            UserInventory currentUsersInventory = await (from ui in _context.UserInventories
                                                         where ui.User.Id == user.Id
                                                         select ui).FirstOrDefaultAsync();

            // Quite possibly redundant code because all users should have an inventory,
            // empty or not.
            if (currentUsersInventory == null)
            {
                TempData["ErrorMessage"] = "User inventory not found";
                return View(deck);
            }

            currentUsersInventory.AddDeck(_context, deck);
            await currentUsersInventory.SaveChanges(_context);

            try
            {
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Deck created successfully";
                return RedirectToAction("ViewAllDecks");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while saving the deck";
                return View(deck);
            }   
        }

        [HttpGet]
        public IActionResult Delete(GameDeck selectedDeck) 
        {
            TempData["DeckId"] = selectedDeck.Id;
            return View(selectedDeck); 
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            int deckId = Convert.ToInt32(TempData["DeckId"]);
            UserInventory currentInventory = new UserInventory();
            GameDeck deckToDelete = await (from d in _context.GameDecks
                                           where d.Id == deckId
                                           select d).FirstOrDefaultAsync();
            currentInventory.RemoveDeck(_context, deckToDelete);
            await currentInventory.SaveChanges(_context);
            return RedirectToAction("ViewAllDecks");
        }
    }
}
