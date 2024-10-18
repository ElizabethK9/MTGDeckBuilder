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

            // Card search result values

            var firstResult = result.Value[0];

            string multiverseIdString = firstResult.MultiverseId;
            int multiverseId = Convert.ToInt32(multiverseIdString);

            string cardName = firstResult.Name;

            string cardType = firstResult.Type;

            string cardSubtype;
            if (firstResult.SubTypes.FirstOrDefault() != null)
            {
                cardSubtype = firstResult.SubTypes.FirstOrDefault();
            }
            else
            {
                cardSubtype = "N/A";
            }

            string manaCost = firstResult.Cmc.ToString();

            string cardSet = firstResult.Set;

            int creaturePower = Convert.ToInt32(firstResult.Power);

            int creatureToughness = Convert.ToInt32(firstResult.Toughness);

            int collectorNumber = Convert.ToInt32(firstResult.Number);

            string cardImageURL = firstResult.ImageUrl.ToString();

            // Pass image url as a cardName object
            GameCard card = new GameCard(multiverseId, cardName, cardType,
                                         cardSubtype, manaCost, cardSet,
                                         creaturePower, creatureToughness, collectorNumber,
                                         cardImageURL);

            return View(card);
        }
    }
}
