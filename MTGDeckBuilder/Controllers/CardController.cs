using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using MtgApiManager.Lib.Service;
using MTGDeckBuilder.Models;
#nullable disable
namespace MTGDeckBuilder.Controllers
{
    // You have to be logged in to access
    [Authorize]
    public class CardController : Controller
    {
        [HttpGet]
        public ActionResult CardSearch()
        {
            CardSearch model = new CardSearch();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CardSearch(CardSearch card)
        {
            if (!ModelState.IsValid)
            {
                TempData["IsValidData"] = "False";
                return View(card);
            }

            await card.PerformCardSearch();

            if (card.SearchResults == null || !card.SearchResults.Any())
            {
                TempData["IsValidData"] = "False";
            }
            else
            {
                TempData["IsValidData"] = "True";
            }

            return View(card);
        }

    }
}
