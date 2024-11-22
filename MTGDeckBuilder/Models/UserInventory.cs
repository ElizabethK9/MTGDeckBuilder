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
            // Update User's card collection in the db
            deck.Inventory = this;
            context.GameDecks.Add(deck);
        }

        /// <summary>
        /// Removes a deck from the current user's deck collection
        /// </summary>
        /// <param name="deck"></param>
        public void RemoveDeck(ApplicationDbContext context, GameDeck deck)
        {
            // Dereference the inventory and cards from the deck 
            deck.Inventory = null;
            foreach (GameCard card in deck.Cards)
            {
                card.GameDeckId = null;
            }

            context.GameDecks.Remove(deck);
        }

        /// <summary>
        /// Adds a card to the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(ApplicationDbContext context, GameCard card)
        {           
            // Update User's card collection in the db
            card.Inventory = this;
            context.GameCards.Add(card);
        }

        /// <summary>
        /// Removes a card from the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(ApplicationDbContext context, GameCard card)
        {
            // Dereference the inventory from the card
            card.Inventory = null;

            // Update User's card collection in the db
            context.GameCards.Remove(card);
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
