using Microsoft.AspNetCore.Identity;
using MTGDeckBuilder.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MTGDeckBuilder.Models
{
    /// <summary>
    /// Custom Microsoft Identity class
    /// </summary>
    public class UserInventory
    {
        [Key] public int Id { get; set; }
        // User that this inventory belongs to
        [ForeignKey("IdentityUser")] public string IdentityUserId { get; set; }
        public IdentityUser IdentityUser
        {
            get; set;
        }
        public List<GameDeck> AllDecks { get; set; }
        public List<GameCard> AllCards { get; set; }
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
            AllDecks.Add(deck);
            // Update User's card collection in the db
            context.GameDecks.Add(deck);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes a deck from the current user's deck collection
        /// </summary>
        /// <param name="deck"></param>
        public void RemoveDeck(ApplicationDbContext context, GameDeck deck)
        {
            AllDecks.Remove(deck);
            // Update User's Deck collection in the db
            context.GameDecks.Remove(deck);
            context.SaveChanges();
        }

        /// <summary>
        /// Adds a card to the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(ApplicationDbContext context, GameCard card)
        {           
            AllCards.Add(card);
            // Update User's card collection in the db
            context.GameCards.Add(card);
            context.SaveChanges();
        }

        /// <summary>
        /// Removes a card from the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(ApplicationDbContext context, GameCard card)
        {
            AllCards.Remove(card);
            // Update User's card collection in the db
            context.GameCards.Remove(card);
            context.SaveChanges();
        }
    }
}
