using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using mtg_lib.Services;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using static mtg_app.Models.SessionExtensions;
using mtg_lib.Library.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;
using System.Web;



namespace mtg_app.Controllers
{
    public class ShopController : Controller
    {
        private CardService cardService = new CardService();

        private readonly ILogger<ShopController> _logger;

        public ShopController(ILogger<ShopController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index(string sortOrder)
        {
            
            switch(sortOrder)
            {
                case "NameAsc":
                    return View(cardService.AllCards().OrderBy(c => c.Name).Take(50));
                case "Rarity":
                    return View(cardService.AllCards().OrderBy(c => c.RarityCode).Take(50));
                case "NameDesc":
                    return View(cardService.AllCards().OrderByDescending(c => c.Name).Take(50));
                case "Type":
                    return View(cardService.AllCards().OrderBy(c => c.Type).Take(50));
                default:
                    return View(cardService.AllCards().Take(50));

            }
        }

        public IActionResult ToDo()
        {
            return View();
        }

        public IActionResult ShoppingCart()
        {
            var model = new ViewModelCart();
            model.cards = new List<Card>();
            
            foreach(string key in HttpContext.Session.Keys){  
                Card card = HttpContext.Session.Get<Card>(key);
                model.cards.Add(card);
            }
            return View(model.cards);
        }

        public IActionResult LikedCards()
        {   
            return View();
        }

        public IActionResult about()
        {
            return View();
        }

        [Route("[action]")]
        public IActionResult GetCardByCardId(int cardId)
        {
            return View(cardService.GetCard(cardId));
        }
        
        [HttpPost]
        public ActionResult AddToCart(int id)
        {
            if (HttpContext.Session.Get<Card>(id.ToString()) == null)
            {   
                Card cardInfo = cardService.GetCard(id);
                HttpContext.Session.Set<Card>(id.ToString(), cardInfo);
            }
            Card card = HttpContext.Session.Get<Card>(id.ToString());
            return View(cardService.GetCard(id));
        }

        public ActionResult ThankYou(){
            HttpContext.Session.Clear();
            return View();
        }

        public ActionResult RemoveCardFromShoppingCart(int id){
            HttpContext.Session.Remove(id.ToString());
            return RedirectToAction("ShoppingCart", "Shop");
        }

        public class ViewModelCart
        {
            public List<Card> cards {get; set; }
        }
    }
}