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
            CardSearch model = new CardSearch();
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> CardSearch(CardSearch card)
        {
            if(ModelState.IsValid)
            {
                await card.PerformCardSearch();
                return View(card);
            }
            return View(card);
        }
    }
}
