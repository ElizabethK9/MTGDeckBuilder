using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MTGDeckBuilder.Data;
using MTGDeckBuilder.Models;
using Microsoft.EntityFrameworkCore;
using MtgApiManager.Lib.Service;
using System.Runtime.InteropServices;
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

            try
            {
                currentUsersInventory.AddDeck(_context, deck);
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
        public IActionResult Delete(int id)
        {
            // Store DeckId in TempData for the POST method
            TempData["DeckId"] = id;
            
            // For display on the delete view
            GameDeck deck = (from d in _context.GameDecks
                        where d.Id == id
                        select d).FirstOrDefault();

            return View(deck);
        }

        [HttpPost]
        public async Task<IActionResult> Delete()
        {
            int deckId = Convert.ToInt32(TempData["DeckId"]);

            // Fetch the deck to delete and ensure it exists
            GameDeck deckToDelete = await (from d in _context.GameDecks
                                      where d.Id == deckId
                                      select d)
                                      .Include(d => d.DeckCards) // Cards won't be populated without this
                                      .FirstOrDefaultAsync();

            // Fetch the user's inventory
            IdentityUser user = await _userManager.GetUserAsync(User);
            UserInventory userInventory = await (from ui in _context.UserInventories
                                       where ui.User.Id == user.Id
                                       select ui).FirstOrDefaultAsync();

            try
            {
                // Remove deck from db and dereference it from its inventory
                userInventory.RemoveDeck(_context, deckToDelete);
                TempData["SuccessMessage"] = "Deck deleted successfully.";
                return RedirectToAction("ViewAllDecks");
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "An error occurred while deleting the deck";
                return View(deckToDelete);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            GameDeck selectedDeck = await _context.GameDecks
                                                  .Where(d => d.Id == id)
                                                  .Include(d => d.DeckCards) // Include all cards associated with the deck
                                                  .ThenInclude(dc => dc.GameCard)
                                                  .FirstOrDefaultAsync();

            // Pass in the deck, then display each card in the deck
            return View(selectedDeck);
        }

        // Adds a card to the deck
        [HttpPost]
        public async Task<IActionResult> Edit(int deckId, string cardSearch)
        {
            // Get the selected deck and its cards
            GameDeck selectedDeck = await _context.GameDecks
                                                   .Include(d => d.DeckCards)
                                                   .ThenInclude(dc => dc.GameCard)
                                                   .FirstOrDefaultAsync(d => d.Id == deckId);

            if (selectedDeck == null)
            {
                TempData["ErrorMessage"] = "Deck not found.";
                return RedirectToAction("ViewAllDecks");
            }

            // Create card search object to perform api call on it
            CardSearch search = new CardSearch();
            search.CardName = cardSearch;
            await search.PerformCardSearch();

            // Ensure that the search results have at least one card
            if (search.SearchResults.Count == 0)
            {
                TempData["ErrorMessage"] = "No cards found";
                return RedirectToAction("Edit", selectedDeck);
            }

            // Take the first result and add it to the db if it doesn't exist
            GameCard firstCard = search.SearchResults.First();
            GameCard cardToAdd = (from c in _context.GameCards
                                  where c.MID == firstCard.MID
                                  select c).FirstOrDefault();

            if (cardToAdd == null)
            {
                // Card does not exist, so add it
                cardToAdd = new GameCard
                {
                    MID = firstCard.MID,
                    Name = firstCard.Name,
                    Type = firstCard.Type,
                    Subtype = firstCard.Subtype,
                    ManaCost = firstCard.ManaCost,
                    Set = firstCard.Set,
                    CreaturePower = firstCard.CreaturePower,
                    CreatureToughness = firstCard.CreatureToughness,
                    CollectorNumber = firstCard.CollectorNumber,
                    ImageURL = firstCard.ImageURL, 
                };

                _context.GameCards.Add(cardToAdd);
                await _context.SaveChangesAsync();
            }

            // Add the GameCard to the GameDeck using DeckCard object
            try
            {
                // Check if the card is already in the deck
                DeckCard existingDeckCard = (from dc in selectedDeck.DeckCards
                                             where dc.GameCardMID == cardToAdd.MID
                                             select dc).FirstOrDefault();

                if (existingDeckCard == null)
                {
                    // Card does not exist in the deck, so add it
                    DeckCard newDeckCard = new DeckCard
                    {
                        GameDeckId = selectedDeck.Id,
                        GameCardMID = cardToAdd.MID,
                        Quantity = 1
                    };

                    _context.DeckCards.Add(newDeckCard);
                }
                else
                {
                    // Card exists in the deck, so just increment the quantity
                    existingDeckCard.Quantity++;
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("Edit", selectedDeck);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "An error occurred while adding the card to the deck.";
                return RedirectToAction("Edit", selectedDeck);
            }
        }
    }
}
