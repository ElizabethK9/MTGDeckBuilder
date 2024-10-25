using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

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
        /// Price of the deck determined by the collective value of each card within the deck.
        /// </summary>
        public int DeckPrice { get; set; }

        /// <summary>
        /// Id of the user that made the deck
        /// </summary>
        public IdentityUser UserID { get; set; }

        /// <summary>
        /// Navigation property for the user.
        /// </summary>
        public virtual IdentityUser User { get; set; }

        /// <summary>
        /// Constructor for a GameDeck object (not including price)
        /// </summary>
        /// <param name="id"></param>
        /// <param name="deckName"></param>
        /// <param name="deckFormat"></param>
        public GameDeck(int id, string deckName, string deckFormat) 
        {
            this.Id = id;
            this.DeckName = deckName;
            this.DeckFormat = deckFormat;
        }
    }
}
