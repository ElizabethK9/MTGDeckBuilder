using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using MtgApiManager.Lib.Service;
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

        private List<GameCard> _searchResults;

        public List<GameCard> SearchResults 
        {
            get 
            {
                return _searchResults ?? new List<GameCard>();
            }
        }

        public async Task PerformCardSearch()
        {
            if (string.IsNullOrWhiteSpace(CardName))
            {
                _searchResults = new List<GameCard>();
                return;
            }

            _searchResults = await ApiCall(CardName);
        }

        private async Task<List<GameCard>> ApiCall(string cardName)
        {
            // Initilize MTG framework
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();

            // Pass in cardSearch string into the framework
            var SearchResults = await service.Where(x => x.Name, CardName)
                                      .AllAsync();

            if (SearchResults == null || !SearchResults.Value.Any())
            {
                return new List<GameCard>();
            }

            // Store 10 of the first non-null search results into a list
            List<GameCard> searchResultsList = new List<GameCard>();
            for (int i = 0;i < SearchResults.Value.Count() && searchResultsList.Count <= 10; i++)
            {
                // Card search result values
                var currentResult = SearchResults.Value[i];
                if (currentResult != null)
                {
                    GameCard card = new GameCard(
                        currentResult.MultiverseId,
                        currentResult.Name,
                        currentResult.Type,
                        currentResult.SubTypes?.FirstOrDefault() ?? "Null",
                        currentResult.Cmc ?? 0f,
                        currentResult.Set,
                        currentResult.Power,
                        currentResult.Toughness,
                        currentResult.Number,
                        currentResult.ImageUrl?.ToString()
                    );

                    if (string.IsNullOrWhiteSpace(currentResult.ImageUrl?.ToString())) 
                    {
                        continue;
                    }

                    searchResultsList.Add(card);                 
                }   
            }
            return searchResultsList;
        }
    }
}
