using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace MTGDeckBuilder.Models
{
    /// <summary>
    /// This object represents a deck that a user creates out of multiple card objects.
    /// Each property is representative of the virtual deck and contains a list of
    /// GameCard objects.
    /// </summary>
    public class GameDeck
    {

        /// <summary>
        /// Unique identifier for each of the user's decks. Start from 0 and increment with every deck.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Name created by the user for their deck.
        /// </summary>
        [Required]
        public string DeckName { get; set; }

        /// <summary>
        /// What format the deck is used for. Specific formats have rules that disallow the same deck to
        /// be used for different formats.
        /// </summary>
        [Required]
        public string DeckFormat { get; set; }

        /// <summary>
        /// Collection of all the cards in the deck
        /// </summary>
        public List<GameCard> Cards { get; set; } = new List<GameCard>();

        /// <summary>
        /// User that owns the deck
        /// </summary>
        public UserInventory Inventory { get; set; }

        // Parameterless constructor for model binding
        public GameDeck() { }

        /// <summary>
        /// Constructor for a GameDeck object (not including price)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deckName"></param>
        /// <param name="deckFormat"></param>
        public GameDeck(string deckName, string deckFormat) 
        {
            this.DeckName = deckName;
            this.DeckFormat = deckFormat;
        }

        /// <summary>
        /// Adds a card to the deck
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(GameCard card)
        {
            Cards.Add(card);
        }

        /// <summary>
        /// Removes a card from the deck
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(GameCard card)
        {
            Cards.Remove(card);
        }
    }
}
