using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MtgApiManager.Lib.Service;
using MTGDeckBuilder.Models;

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
            String cardSearch = TempData["Card"] as String;

            // Initilize MTG framework
            IMtgServiceProvider serviceProvider = new MtgServiceProvider();
            ICardService service = serviceProvider.GetCardService();

            // Pass in cardSearch string into the framework
            var result = await service.Where(x => x.Name, cardSearch)
                                      .AllAsync();

            // Card search result values         
            string cardImageLink = result.Value[0].ImageUrl.ToString();

            // Pass image url as a cardName object
            GameCard card = new GameCard(cardImageLink);

            return View(card);
        }
    }
}
