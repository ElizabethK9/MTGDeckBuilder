using System.ComponentModel.DataAnnotations;

namespace MTGDeckBuilder.Models
{
    /// <summary>
    /// This object represents a single card for Magic the Gathering
    /// and each of the object's properties represents a different
    /// part of the real life card.
    /// </summary>
    public class GameCard
    {
        /// <summary>
        /// Multiverse id of a card. Multiverse id is a unique id for each card
        /// in Magic that refers to each cards printed order from each set.
        /// </summary>
        public int? CardMID { get; set; }

        /// <summary>
        /// Fullname of the card
        /// </summary>
        [Required]
        public string CardName { get; set; }

        /// <summary>
        /// Card type represents what kind of card it is. Magic has many card types like
        /// creatures, instants, sorceries, enchantments and more.
        /// </summary>
        public string? CardType { get; set; }

        /// <summary>
        /// Several cards have a subtype. Creatures are the best example of this with creature
        /// being the main type, and humans or beasts being its subtype.
        /// </summary>
        public string? CardSubtype { get; set; }

        /// <summary>
        /// All cards have a converted mana cost (referred to as CMC a lot of times). The cost could include any combination
        /// of the five main colors, or colorless mana costs. There are even cards with mana cost 0, or cards
        /// that don't apppear to have a mana cost, like tokens, which have a hidden mana cost of 0.
        /// </summary>
        public string? ManaCost { get; set; }

        /// <summary>
        /// A card's set is what set the card is released in. There are an abundance of card reprints in later sets,
        /// so a card can appear in more sets than just one. Set examples are Aplha, Return to Innistrad, Theros,
        /// Unglued, and more. There are over 100 card sets in Magic as the series debuted in 1993 and has been going
        /// strong since.
        /// </summary>
        public string? CardSet { get; set; }

        /// <summary>
        /// Will be implemented in the future when the site is more fleshed out.
        /// </summary>
        public int? CardPrice { get; set; }

        /// <summary>
        /// Int value for the attacking power of the card if it's a creature. Can include 0.
        /// </summary>
        public int? CreaturePower { get; set; }

        /// <summary>
        /// Int value for the defensive power of the card if it's a creature. Can include 0.
        /// </summary>
        public int? CreatureToughness { get; set; }

        /// <summary>
        /// Unlinke the mulitverse id, collector number represents the order the
        /// card was printed from the specific set
        /// </summary>
        public int? CollectorNumber { get; set; }

        // Parameterless constructor
        public GameCard() { }

        /// <summary>
        /// Full constructor for the game card
        /// </summary>
        /// <param name="cardMID"></param>
        /// <param name="cardName"></param>
        /// <param name="cardType"></param>
        /// <param name="cardSubtype"></param>
        /// <param name="manaCost"></param>
        /// <param name="cardSet"></param>
        /// <param name="cardPrice"></param>
        /// <param name="creaturePower"></param>
        /// <param name="creatureToughness"></param>
        /// <param name="collectorNumber"></param>
        public GameCard(int cardMID, string cardName, string cardType,
                        string cardSubtype, string manaCost, string cardSet,
                        int cardPrice, int creaturePower, int creatureToughness, 
                        int collectorNumber)
        {
            this.CardMID = cardMID;
            this.CardName = cardName;
            this.CardType = cardType;
            this.CardSubtype = cardSubtype;
            this.ManaCost = manaCost;
            this.CardSet = cardSet;
            this.CardPrice = cardPrice;
            this.CreaturePower = creaturePower;
            this.CreatureToughness = creatureToughness;
            this.CollectorNumber = collectorNumber;
        }

        public GameCard(string cardName) 
        {
            this.CardName=cardName;
        }
    }
}
