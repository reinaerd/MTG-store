using System;
using mtg_lib.Library.Models;
namespace password 
{
    class Program
    {
        static void Main(string[] args)
        {
            MTGdatabaseContext context = new MTGdatabaseContext();
            
            var cardsList = context.Cards
                    .OrderBy(c => c.Name)
                    .ToList();
            
            foreach(var item in cardsList){
                Console.WriteLine(item.Name);
            }
        }
    }
}