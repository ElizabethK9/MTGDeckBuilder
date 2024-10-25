using System.ComponentModel.DataAnnotations;

namespace MTGDeckBuilder.Models
{
    /// <summary>
    /// View model for the card search view
    /// </summary>
    public class CardSearch
    {
        /// <summary>
        /// The user's search as a string
        /// </summary>
        [Required]
        public string? CardName { get; set; }
    }
}
