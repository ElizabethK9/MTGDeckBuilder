namespace MTGDeckBuilder.Models
{
    // This model allows us to track how many cards are in each deck
    public class DeckCard
    {
        public int GameDeckId { get; set; }
        public GameDeck GameDeck { get; set; }

        public string GameCardMID { get; set; }
        public GameCard GameCard { get; set; }

        public int Quantity { get; set; }
    }
}
