using Microsoft.AspNetCore.Identity;

namespace MTGDeckBuilder.Models
{
    public class User : IdentityUser
    {
        public List<GameDeck> AllDecks { get; set; }

        public List<GameCard> AllCards { get; set; }

        /// <summary>
        /// Adds a deck to the current user's deck collection
        /// </summary>
        /// <param name="deck"></param>
        public void AddDeck(GameDeck deck)
        {
            AllDecks.Add(deck);
        }

        /// <summary>
        /// Removes a deck from the current user's deck collection
        /// </summary>
        /// <param name="deck"></param>
        public void RemoveDeck(GameDeck deck)
        {
            AllDecks.Remove(deck);
        }

        /// <summary>
        /// Adds a card to the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void AddCard(GameCard card)
        {
            AllCards.Add(card);
        }

        /// <summary>
        /// Removes a card from the current user's card collection
        /// </summary>
        /// <param name="card"></param>
        public void RemoveCard(GameCard card)
        {
            AllCards.Remove(card);
        }
    }
}
