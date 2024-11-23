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
    }
}
