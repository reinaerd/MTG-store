using System;
using System.Linq;
using System.Collections.Generic;
using mtg_lib.Library.Models;

namespace mtg_lib.Services
{
    public class CardService
    {
        MTGdatabaseContext context = new MTGdatabaseContext();

        public Card GetCard(int cardId){
            var results = context.Cards.Where(c => c.Id == cardId);
            if(results.Count() == 0){
                return null;
            }
            
            return results.First();
            
        }
        public IEnumerable<Card> AllCards(){
            return context.Cards.OrderBy(c => c.Name);
        }
    }
}
