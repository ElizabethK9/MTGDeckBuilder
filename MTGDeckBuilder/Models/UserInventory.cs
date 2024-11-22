using Microsoft.AspNetCore.Identity;
using MtgApiManager.Lib.Model;
using MTGDeckBuilder.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#nullable disable
namespace MTGDeckBuilder.Models
{
    /// <summary>
    /// Custom Microsoft Identity class
    /// </summary>
    public class UserInventory
    {
        [Key] public int Id { get; set; }
        [ForeignKey("IdentityUser")] 
        public string IdentityUserId { get; set; }
        public List<GameDeck> AllDecks { get; set; }
        public List<GameCard> AllCards { get; set; }
        // User that this inventory belongs to
        public IdentityUser User{ get; set; } 
        public UserInventory()
        {
            AllDecks = new List<GameDeck>();
            AllCards = new List<GameCard>();
        }
        /// <summary>
        /// Adds a deck to the current user's deck collection
        /// </summary>
        /// <param name="deck"></param>
        public void AddDeck(ApplicationDbContext context, GameDeck deck)
        {
            // Dereference the inventory and cards from the deck
            deck.Inventory = this;

            context.GameDecks.Add(deck);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes a deck from the current user's deck collection
        /// </summary>
        /// <param name="deck"></param>
        public void RemoveDeck(ApplicationDbContext context, GameDeck deck)
        {
            // Dereference the inventory and cards from the deck 
            deck.Inventory = null;

            // Remove associated DeckCards
            foreach (DeckCard deckCard in deck.DeckCards.ToList())
            {
                context.DeckCards.Remove(deckCard);
            }
            context.GameDecks.Remove(deck);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a card to the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(ApplicationDbContext context, GameCard card)
        {
            // Dereference the inventory from the card
            card.Inventory = this;

            context.GameCards.Add(card);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes a card from the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(ApplicationDbContext context, GameCard card)
        {
            // Dereference the inventory from the card
            card.Inventory = null;

            context.GameCards.Remove(card);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a card to a specific deck in the current user's collection
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="card"></param>
        public void AddCardToDeck(ApplicationDbContext context, GameDeck deck, GameCard card)
        {
            // Associate the deck with this inventory
            deck.Inventory = this;

            // Create a new DeckCard if it doesnt already exist
            DeckCard deckCard = new DeckCard
            {
                GameDeckId = deck.Id,
                GameCardMID = card.MID,
                Quantity = 1
            };

            context.DeckCards.Add(deckCard);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes a card from a specific deck in the current user's collection
        /// </summary>
        /// <param name="deck"></param>
        /// <param name="card"></param>
        public void RemoveCardFromDeck(ApplicationDbContext context, GameDeck deck, GameCard card)
        {
            // Find the deckcard where the MID's and Deck Id's are equal
            DeckCard deckCard = (from dc in context.DeckCards
                                 where dc.GameDeckId == deck.Id && dc.GameCardMID == card.MID
                                 select dc).FirstOrDefault();

            if (deckCard != null)
            {
                context.DeckCards.Remove(deckCard);
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Saves changes made to the user's inventory in the database.
        /// </summary>
        public async Task SaveChanges(ApplicationDbContext context)
        {
           await context.SaveChangesAsync();
        }
    }
}
