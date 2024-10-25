using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MtgApiManager.Lib.Service;
using MTGDeckBuilder.Models;
#nullable disable
namespace MTGDeckBuilder.Controllers
{
    public class CardController : Controller
    {
        [HttpGet]
        public ActionResult CardSearch()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CardSearch(GameCard card)
        {
            if(ModelState.IsValid)
            {
                TempData["Card"] = card.CardName;
                return RedirectToAction("ViewCard");
            }
            return View(card);
        }

        public async Task<ActionResult> ViewCard()
        {
            // User entered string for card search
            string cardSearch = TempData["Card"] as string;

            // Initilize MTG framework
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();

            // Pass in cardSearch string into the framework
            var result = await service.Where(x => x.Name, cardSearch)
                                      .AllAsync();

            if (result == null || !result.Value.Any())
            {
                return NotFound("No cards found.");
            }

            // Card search result values
            var firstResult = result.Value.FirstOrDefault();
            if (firstResult == null)
            {
                return NotFound("Card not found.");
            }

            string cardSubtype = firstResult.SubTypes?.FirstOrDefault() ?? "N/A";
            int creaturePower = firstResult.Power != null ? Convert.ToInt32(firstResult.Power) : 0;
            int creatureToughness = firstResult.Toughness != null ? Convert.ToInt32(firstResult.Toughness) : 0;

            GameCard card = new GameCard(
                Convert.ToInt32(firstResult.MultiverseId),
                firstResult.Name,
                firstResult.Type,
                cardSubtype,
                firstResult.Cmc.ToString(),
                firstResult.Set,
                creaturePower,
                creatureToughness,
                Convert.ToInt32(firstResult.Number),
                firstResult.ImageUrl?.ToString()
            );

            return View(card);
        }
    }
}
